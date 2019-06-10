using ngxsis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ngxsis.DataModel;

namespace ngxsis.Repository
{
   public class GetAll
    {

        public static List<DropDownModel> AllReligion()
        {

            List<DropDownModel> result = new List<DropDownModel>();
            using (var db = new ngxsisContext())
            {

                result = db.x_religion.Select(c => new DropDownModel
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                }).ToList();


            }

            return result;

        }

        public static List<DropDownModel> AllIdentity()
        {

            List<DropDownModel> result = new List<DropDownModel>();
            using (var db = new ngxsisContext())
            {

                result = db.x_identity_type.Select(c => new DropDownModel
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                }).ToList();


            }

            return result;

        }

        public static List<DropDownModel> AllMarital()
        {

            List<DropDownModel> result = new List<DropDownModel>();
            using (var db = new ngxsisContext())
            {

                result = db.x_marital_status.Select(c => new DropDownModel
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                }).ToList();


            }

            return result;

        }


    }
}
