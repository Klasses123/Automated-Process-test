using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CostsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //создал базу, в которую залил исходную таблицу, создал модель базы, работаю с ней через EntityFramework
            //с помощью C#, Linq выражений
            //скрипт не складывает стоимость в одно число, насколько я понял, записывается стоимость за день
            using (CostsDB db = new CostsDB())
            {
                Entity startPeriod = null;
                //пока есть не сокращенные записи, "склеиваем"
                while (HasContinue(db) != null)
                {
                    //находит запись, к которой можно приклеить другую
                    startPeriod = HasContinue(db);

                    //ищет запись сверху(по дате)
                    var endPeriod = db.Entities
                        .Where(x => x.StartDate == startPeriod.EndDate &&
                            startPeriod.AdultsMainWeekday == x.AdultsMainWeekday &&
                            startPeriod.AdultsMainWeekend == x.AdultsMainWeekend)
                        .FirstOrDefault();

                    //если не нашёл, ищет запись снизу(по дате)
                    if(endPeriod == null)
                    {
                        endPeriod = db.Entities
                            .Where(x => x.EndDate == startPeriod.StartDate &&
                                startPeriod.AdultsMainWeekday == x.AdultsMainWeekday &&
                                startPeriod.AdultsMainWeekend == x.AdultsMainWeekend)
                            .FirstOrDefault();

                        //удаляет обе записи, создает на их основу одну
                        db.Entities.Remove(startPeriod);
                        db.Entities.Remove(endPeriod);
                        startPeriod.StartDate = endPeriod.StartDate;
                        db.SaveChanges();
                    }
                    else
                    {
                        //удаляет обе записи, создает на их основу одну
                        db.Entities.Remove(startPeriod);
                        db.Entities.Remove(endPeriod);
                        startPeriod.EndDate = endPeriod.EndDate;
                        db.SaveChanges();
                    }
                    //вносит новую запись в базу
                    db.Entities.Add(startPeriod);
                    db.SaveChanges();
                }
                //выводим в консоль итоговое кол-во записей(получилось 28)
                Console.WriteLine(db.Entities.Count());
            }
            Console.ReadKey();
        }

        //метод поиска записей, которые возможно "склеить"
        static Entity HasContinue(CostsDB db)
        {
            return db.Entities
                .Where(x => db.Entities
                .Where(y => y.EndDate == x.StartDate && y.AdultsMainWeekday == x.AdultsMainWeekday &&
                    y.AdultsMainWeekend == x.AdultsMainWeekend).FirstOrDefault().ID != null)
                .FirstOrDefault();
        }
    }
}
