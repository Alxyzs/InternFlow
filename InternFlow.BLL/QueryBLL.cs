using InternFlow.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternFlow.BLL
{
    public class QueryBLL
    {

        private readonly QueryDAL _dal;

        public QueryBLL(QueryDAL dal)
        {
            _dal = dal;
        }


    }
}
