﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Armamentos.
    /// </summary>
    public sealed class FornecimentoDiarioCanaCollection : BaseCollection<FornecimentoDiarioCana>, INFeSerializable
    {
        private const int Capacidade = 31;

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get { return this.Any(x => x.IsDirty); }
        }

        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            foreach (var item in this)
            {
                if (item.IsDirty)
                    ((INFeSerializable)item).Serialize(writer, nfe);
            }
        }

        protected override void PreAdd(CancelEventArgs e, FornecimentoDiarioCana item)
        {
            if (Count == Capacidade)
                throw new ApplicationException(
                    string.Format("A capacidade máxima deste campo é de {0} Fornecimentos Diários de Cana.", Capacidade));
            base.PreAdd(e, item);
        }
    }
}