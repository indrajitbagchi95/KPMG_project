using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;

namespace KPMG.Models
{
    public class Customer
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Phone")]
        public string Phone { get; set; }
        public List<Customer> CustomerList { get; set; }
        public void GetCustomerList()
        {
            CustomerList = new List<Customer>();
            try
            {
                DataTable dt = Utility.ExecuteQuery(ConfigurationManager.
            ConnectionStrings["myConnectionString"].ConnectionString, "select * from Customer");
                foreach (DataRow dr in dt.Rows)
                {
                    Customer obj = new Customer();

                    obj.FirstName = dr["FirstName"].ToString();
                    obj.LastName = dr["LastName"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.Country = dr["Country"].ToString();
                    obj.Phone = dr["Phone"].ToString();

                    CustomerList.Add(obj);
                }
            }
            catch (Exception ex)
            {
                //Exception logging
            }
        }
    }

    public class ChartData
    {
        public string Country { get; set; }
        public string Count { get; set; }
        public List<ChartData> ChartList { get; set; }
        public void GetChartData()
        {
            ChartList = new List<ChartData>();
            try
            {
                DataTable dt = Utility.ExecuteQuery(ConfigurationManager.
            ConnectionStrings["myConnectionString"].ConnectionString, "select Country, count(*) as count_data from Customer group by Country order by Country");
                foreach (DataRow dr in dt.Rows)
                {
                    ChartData obj = new ChartData();

                    obj.Country = dr["Country"].ToString();
                    obj.Count = dr["count_data"].ToString();
                    ChartList.Add(obj);
                }
            }
            catch (Exception ex)
            {
                //Exception logging
            }
        }
    }
   
}