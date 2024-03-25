﻿using MauiListaCompras.Models;
using SQLite;

namespace MauiListaCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path) 
        {
            _conn =new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task <int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDADE Produto SET descricao=?, " + "Quantidade =?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto> ().DeleteAsync(
                i => i.Id == id);
        }

        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * FROM Produto WHERE" + "Descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto> (sql);
        }

    }// fecha classe
}//fecha nomespace
