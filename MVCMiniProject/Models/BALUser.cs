using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MVCMiniProject.Models
{
    public class BALUser
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-B3UBKFN;Initial Catalog=MVCMiniProject;Integrated Security=True");
        public void Register(string Name,string EmailId,int CityId,string Password)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Register");
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@emailid", EmailId);
            cmd.Parameters.AddWithValue("@cityid", CityId);
            cmd.Parameters.AddWithValue("@password", Password);
            
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SaveImage(string image1, HttpPostedFileBase image2)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);

           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "SaveImage");
            cmd.Parameters.AddWithValue("@Image1", image1);
            cmd.Parameters.AddWithValue("@Image2", image2);
            
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataSet GetCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetCountry");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

            con.Close();
        }
        public DataSet GetState(int CountryId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetState");
            cmd.Parameters.AddWithValue("@countryId", CountryId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

            con.Close();
        }
        public DataSet GetCity(int StateId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetCity");
            cmd.Parameters.AddWithValue("@stateId", StateId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

            con.Close();
        }
        public DataSet GetDetails()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetDetails");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public void Delete(int RegisterId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Delete");
            cmd.Parameters.AddWithValue("@registerId",RegisterId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader Fetch(int RegisterId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Fetch");
            cmd.Parameters.AddWithValue("@registerId", RegisterId);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public void Edit(string Name, string EmailId, int CityId, string Password,int RegisterId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("MINIMVCSP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Edit");
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@emailid", EmailId);
            cmd.Parameters.AddWithValue("@cityid", CityId);
            cmd.Parameters.AddWithValue("@password", Password);
            cmd.Parameters.AddWithValue("@registerId", RegisterId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}