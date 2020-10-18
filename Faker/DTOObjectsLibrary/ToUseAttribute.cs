using System;
using System.Collections.Generic;
using System.Text;

namespace DTOObjectsLibrary
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ToUseAttribute : Attribute
    {
    }
}
