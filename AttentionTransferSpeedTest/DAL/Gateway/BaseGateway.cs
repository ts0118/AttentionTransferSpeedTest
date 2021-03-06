﻿using MySql.Data.MySqlClient;
using System;

namespace AttentionTransferSpeedTest.DAL.Gateway
{
    internal abstract class BaseGateway
    {
        /// <summary>
          /// 建立mysql数据库链接
          /// </summary>
          /// <returns></returns>
        public static MySqlConnection getMySqlCon()
        {
            String mysqlStr = "server=localhost;user id=root;password=;database=mysql";

            // String mySqlCon = ConfigurationManager.ConnectionStrings["MySqlCon"].ConnectionString;
            MySqlConnection mysql = new MySqlConnection(mysqlStr);
            MySqlCommand cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS test;", mysql);
            mysqlStr = "server=localhost;user id=root;password=;database=test;Charset=utf8";
            mysql = new MySqlConnection(mysqlStr);
            return mysql;
        }

        /// <summary>
          /// 建立执行命令语句对象
          /// </summary>
          /// <param name="sql"></param>
          /// <param name="mysql"></param>
          /// <returns></returns>
        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            // MySqlCommand mySqlCommand = new MySqlCommand(sql);
            // mySqlCommand.Connection = mysql;
            return mySqlCommand;
        }

        /// <summary>
          /// 查询并获得结果集并遍历
          /// </summary>
          /// <param name="mySqlCommand"></param>
        public abstract void getResultset(MySqlCommand mySqlCommand);

        /// <summary>
          /// 添加数据
          /// </summary>
          /// <param name="mySqlCommand"></param>
        public static void getInsert(string tableName, string[] values, MySqlConnection mysql)
        {
            try
            {
                string queryString = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";
                for (int i = 1; i < values.Length; i++)
                {
                    queryString += ", " + "'" + values[i] + "'";
                }
                queryString += " )";
                MySqlCommand mySqlCommand = new MySqlCommand(queryString, mysql);
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                String message = ex.Message;
                Console.WriteLine("插入数据失败了！" + message);
            }
        }

        /// <summary>
          /// 修改数据
          /// </summary>
          /// <param name="mySqlCommand"></param>
        public static void getUpdate(MySqlCommand mySqlCommand)
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                String message = ex.Message;
                Console.WriteLine("修改数据失败了！" + message);
            }
        }

        /// <summary>
          /// 删除数据
          /// </summary>
          /// <param name="mySqlCommand"></param>
        public static void getDel(MySqlCommand mySqlCommand)
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                String message = ex.Message;
                Console.WriteLine("删除数据失败了！" + message);
            }
        }

        public void CreateTable(string tableName, string[] colNames, string[] colTypes, MySqlConnection mysql)
        {
            string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            queryString += "  ) ";

            MySqlCommand mySqlCommand = new MySqlCommand(queryString, mysql);
            mySqlCommand.ExecuteNonQuery();
        }
    }
}