﻿using billing_software.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billing_software.DAL
{
    internal class userDAL
    {
      static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
      
      #region Select Data from Database
      public DataTable Select()
        {

         SqlConnection conn = new SqlConnection(myconnstring);

         DataTable dt = new DataTable();
         try{
            String sql = "SELECT * FROM [fatj].[dbo].[tbl_user]";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            conn.Open();
            adapter.Fill(dt);
         }
         catch(Exception ex){
            MessageBox.Show(ex.Message);
         }
         finally{
            conn.Close();
         }
         return dt;
      }
      #endregion

      #region Insert Data in Database 
        public bool Insert(userBLL u){      
           bool isSuccess = false;
           SqlConnection conn = new SqlConnection(myconnstring);
           try{
                String sql = "INSERT INTO [fatj].[dbo].[tbl_user] (first_name,last_name,email,username,password,contact,address,gender,user_type,added_date,added_by) VALUES (@first_name,@last_name,@email,@username,@password,@contact,@address,@gender,@user_type,@added_date,@added_by)";
                
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type); 
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();

                int rows=cmd.ExecuteNonQuery();

                if(rows>0){
                    //Query Successful
                    isSuccess = true;
                }
                else{
                    //Query Failed
                    isSuccess = false;
                }
           }
           catch(Exception ex){
               MessageBox.Show(ex.Message);
           }
           finally{
               conn.Close();
           }
           return isSuccess;
        }
      #endregion

      #region Update data in Database 

        public bool Update(userBLL u){
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);

            try{
                string sql = "UPDATE [fatj].[dbo].[tbl_user] SET first_name=@first_name,last_name=@last_name,email=@email,username=@username,password=@password,contact=@contact,address=@address,gender=@gender,user_type=@user_type,added_date=@added_date,added_by=@added_by WHERE id=@id";
 
                SqlCommand cmd = new SqlCommand(sql,conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type); 
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);  

                conn.Open();

                int rows=cmd.ExecuteNonQuery();

                if(rows>0){
                    //Query Successful
                    isSuccess = true;
                }
                else{
                    //Query Failed
                    isSuccess = false;
                }
           }
           catch(Exception ex){
               MessageBox.Show(ex.Message);
           }
           finally{
               conn.Close();
           }
           return isSuccess;
        }

      #endregion

      #region Delete data from database  
        
        public bool Delete(userBLL u){
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);


            try{
                string sql = "DELETE FROM [fatj].[dbo].[tbl_user] WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open(); 
                int rows=cmd.ExecuteNonQuery();

                if(rows>0){
                    //Query Successful
                    isSuccess = true;
                }
                else{
                    //Query Failed
                    isSuccess = false;
                }
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
            finally{
                conn.Close();
            }
            return isSuccess;
        }


      #endregion

      #region Search User on database usingKeywords
        public DataTable Search(string keywords)
        {

         SqlConnection conn = new SqlConnection(myconnstring);

         DataTable dt = new DataTable();
         try{
            String sql = "SELECT * FROM [fatj].[dbo].[tbl_user] WHERE id Like '%"+keywords+"%' OR first_name LIKE '%"+keywords+"%' OR last_name LIKE '%"+keywords+"%' OR username LIKE '%"+keywords+"%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            conn.Open();
            adapter.Fill(dt);
         }
         catch(Exception ex){
            MessageBox.Show(ex.Message);
         }
         finally{
            conn.Close();
         }
         return dt;
      }
      #endregion


      #region getting userid from user name 

      public userBLL GetIDFromUsername (string username){

      userBLL u = new userBLL();

      SqlConnection conn = new SqlConnection(myconnstring);

      DataTable dt = new DataTable();
    
      try{
        string sql = "SELECT id FROM [fatj].[dbo].[tbl_user] WHERE username='"+username+"'";
          
        SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
        conn.Open();

        adapter.Fill(dt);
        if(dt.Rows.Count>0){
          u.id = int.Parse(dt.Rows[0]["id"].ToString());
        }

      }
      catch (Exception ex){
         MessageBox.Show(ex.Message);
      }
      finally{
        conn.Close();
      }
      return u;
     }
   

#endregion
      
    }
}
