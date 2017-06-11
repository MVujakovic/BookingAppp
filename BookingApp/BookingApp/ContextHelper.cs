using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace BookingApp
{
    public class ContextHelper
    {
        public static void SaveChanges(DbContext context)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("\n__________________________________\nContextHelper.SaveChanges() debug:\n");

                context.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);

                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);               
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: \n\t", fullErrorMessage);

                Debug.Print(exceptionMessage);

                //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
        }
    }
}