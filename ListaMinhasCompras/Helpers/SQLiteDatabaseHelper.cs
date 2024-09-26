using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaMinhasCompras.Models;
using SQLite;

namespace ListaMinhasCompras.Helpers
{
    /*
         * Criando a classe SQLiterDatabaseHelper
         * Classe reponsavel pelas as operacoes sql (CRUD)
    */
    public class SQLiteDatabaseHelper
    {
        //Habilita uma conexão com o banco de dados.
        readonly SQLiteAsyncConnection _conn;


        //Iniciar a conexão com o banco no construtor
        public SQLiteDatabaseHelper(string path)
        {
            //Path e o caminho do arquivo sqlite
            /*
             *  Cria um objeto e passa como parametro o arquivo/conexão com o banco de dados.
            */
            _conn = new SQLiteAsyncConnection(path);
            
            _conn.CreateTableAsync<Produto>().Wait();
        }

        //Metodos de manipulação de dados recebem o produto como parametros
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {

            string sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE = ?";

            return _conn.QueryAsync<Produto>(
               sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * Produto WHERE Descricao Like %"+q+"%";
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
