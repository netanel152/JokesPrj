﻿using JokesPrj.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace JokesPrj.DAL
{
    public class JokeDAL
    {
        private readonly string conStr;
        public JokeDAL(string conStr)
        {
            this.conStr = conStr;
        }
        public int SaveNewJokeToDB(Joke j)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    string query = $"Insert into Jokes (id_user,joke_title,joke_body,joke_img,username,user_img) VALUES (@id_user,@joke_title,@joke_body,@joke_img,@username,@user_img)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_user", SqlDbType.Int).Value = j.Id_user;
                    cmd.Parameters.AddWithValue("@joke_title", SqlDbType.NVarChar).Value = j.Joke_title;
                    cmd.Parameters.AddWithValue("@joke_body", SqlDbType.NVarChar).Value = j.Joke_body;
                    cmd.Parameters.AddWithValue("@joke_img", SqlDbType.NVarChar).Value = j.Joke_img;
                    cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = j.Username;
                    cmd.Parameters.AddWithValue("@user_img", SqlDbType.NVarChar).Value = j.User_img;
                    int res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Joke GetJoke(Joke j)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    List<Joke> jokeList = new List<Joke>();
                    string query = $"SELECT * FROM Jokes WHERE joke_title= @joke_title";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@joke_title", j.Joke_title);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        j = new Joke(
                            Convert.ToInt32(reader["id_joke"]),
                            Convert.ToInt32(reader["id_user"]),
                            Convert.ToString(reader["joke_title"]),
                            Convert.ToString(reader["joke_body"]),
                            Convert.ToString(reader["joke_img"]),
                            Convert.ToString(reader["username"]),
                            Convert.ToString(reader["user_img"])
                            );
                    return j;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Joke> GetAllJokes(Joke j)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    List<Joke> jokeList = new List<Joke>();
                    string query = $"SELECT * FROM Jokes WHERE joke_title= @joke_title";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@joke_title", j.Joke_title);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader == null)
                    {
                        return jokeList;
                    }
                    while (reader.Read())
                    {
                        j = new Joke(
                            Convert.ToInt32(reader["id_joke"]),
                            Convert.ToInt32(reader["id_user"]),
                            Convert.ToString(reader["joke_title"]),
                            Convert.ToString(reader["joke_body"]),
                            Convert.ToString(reader["joke_img"]),
                            Convert.ToString(reader["username"]),
                            Convert.ToString(reader["user_img"])
                            );
                        jokeList.Add(j);
                    }
                    return jokeList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Joke> GetAllJokes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {

                    List<Joke> jokes = new List<Joke>();
                    Joke j = null;
                    string query = "select * from Jokes";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        j = new Joke(
                            Convert.ToInt32(reader["id_joke"]),
                            Convert.ToInt32(reader["id_user"]),
                            Convert.ToString(reader["joke_title"]),
                            Convert.ToString(reader["joke_body"]),
                            Convert.ToString(reader["joke_img"]),
                            Convert.ToString(reader["username"]),
                            Convert.ToString(reader["user_img"])
                            );
                        jokes.Add(j);
                    }
                    return jokes;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
