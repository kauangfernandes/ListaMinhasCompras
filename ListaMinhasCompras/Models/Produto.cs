using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;


namespace ListaMinhasCompras.Models
{
    public class Produto
    {
        string? _descricao;
        double _quantidade;
        double _preco;

        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }


        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Descrição inválida");

                    _descricao = value;
                }
            }
        }

        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (!double.TryParse(value.ToString(), out _quantidade))
                {
                    _quantidade = 0;
                }

                if (value == 0 || value < 0)
                {
                    throw new Exception("Quantidade inválida");
                }

                _quantidade = value;
            }
        }

        public double Preco
        {
            get => _preco;
            set
            {
                if (!double.TryParse(value.ToString(), out _preco))
                {
                    _preco = 0;
                }

                if (value <= 0)
                {
                    throw new Exception("Valor inválida");
                }

                _preco = value;
            }
        }

        public double Total
        {
            get => Preco * Quantidade;
        }
    }
}
