using System;
using System.ComponentModel.DataAnnotations;

namespace ADO.Business.Models
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }

        public DateTime Cadastrado_em { get; set; }

        public Entity()
        {
            Cadastrado_em = DateTime.Now;
        }

        //public DateTime Cadastrado_em
        //{
        //    get { return _cadastrado_em; }
        //    set { _cadastrado_em =  DateTime.Now; }

        //}

    }


}

