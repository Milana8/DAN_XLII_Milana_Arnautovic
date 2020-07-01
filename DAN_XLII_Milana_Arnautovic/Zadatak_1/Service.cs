using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1
{
    public class Service
    {
        public List<vwEmployee> GetAllEmployees()
        {
            try
            {
                using (DAN_XLIIEntities1 context = new DAN_XLIIEntities1())
                {
                    List<vwEmployee> list = new List<vwEmployee>();
                    list = (from x in context.vwEmployees select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        ///// <summary>
        ///// This method saves location into database
        ///// </summary>
        ///// <param name="location"></param>
        ///// <returns></returns>
        //public tblLocation AddLocation(tblLocation location)
        //{
        //    try
        //    {
        //        using (DAN_XLIIEntities1 context = new DAN_XLIIEntities1())
        //        {
        //            tblLocation newLocation = new tblLocation();
        //            newLocation.Adress = location.Adress;
        //            newLocation.Place = location.Place;
        //            newLocation.States = location.States;

        //            context.tblLocations.Add(newLocation);
        //            context.SaveChanges();
        //            //Write.Writer.WriteLocation(newLocation);
        //            return location;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
        //        return null;
        //    }
        //}
        /// <summary>
        /// This method gets locations from database and inserts them into list
        /// </summary>
        /// <returns></returns>
        public List<tblLocation> GetAllLocations()
        {
            try
            {
                using (DAN_XLIIEntities1 context = new DAN_XLIIEntities1())
                {
                    List<tblLocation> list = new List<tblLocation>();
                    list = (from x in context.tblLocations select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
