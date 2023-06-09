﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho01_ClubeDaLeitura.ConsoleApp.Compartilhado;
using Trabalho01_ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using Trabalho01_ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using Trabalho01_ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace Trabalho01_ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class TelaCadastroRevistas : Tela
    {
        public RepositorioRevistas repositorioRevistas = null;
        public RepositorioCaixas repositorioCaixas = null;
        public TelaCadastroCaixa telaCadastroCaixa = null;

        public void EscolherOpcaoMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                MostrarMenu("Revista", ConsoleColor.DarkMagenta);

                continuar = InicializarOpcaoEscolhida();
            }
        }

        public void VisualizarRevistas()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("╔" + "".PadRight(117, '═') + "╗");
            Console.WriteLine("║                                                      Revistas                                                       ║");
            Console.WriteLine("╚" + "".PadRight(117, '═') + "╝");
            PulaLinha();
            Console.ForegroundColor = ConsoleColor.Magenta;
            string espacamento = "{0, -5} │ {1, -30} │ {2, -25} │ {3, -12} │ {4, -7} │ {5, -25}";
            Console.WriteLine(espacamento, "ID", "Título", "Tipo de Coleção", "N°Edição", "Ano", "Caixa");
            Console.WriteLine("".PadRight(119, '―'));
            Console.ResetColor();
            foreach (Revistas info in repositorioRevistas.GetListaDados())
            {
                TextoZebrado();

                Console.WriteLine(espacamento, info.id, info.titulo, info.colecao, info.edicao, info.ano, info.caixa == null ? "<Sem Caixa>" : info.caixa.etiqueta);
            }

            Console.ResetColor();
            zebra = true;

            PulaLinha();
        }

        private void AdicionarCadastroRevista()
        {
            VisualizarRevistas();

            Revistas infoRevista = ObterCadastroRevista();

            repositorioRevistas.Adicionar(infoRevista);

            VisualizarRevistas();

            MensagemColor("\nRevista adicionada com sucesso!", ConsoleColor.Green);

            Console.ReadLine();
        }

        private void EditarCadastroRevista()
        {
            VisualizarRevistas();

            if (ValidaListaVazia(repositorioRevistas.listaDados))
            {
                Revistas idSelecionado; bool idValido;
                do
                {
                    idSelecionado = (Revistas)repositorioRevistas.SelecionarId(ObterIdEscolhido("Digite o ID da Revista que deseja editar: "));

                    idSelecionado = (Revistas)ValidaId(idSelecionado);

                    idValido = idSelecionado != null;

                } while (!idValido);

                Revistas infoRevistaAtualizado = ObterCadastroRevista();

                repositorioRevistas.Editar(idSelecionado, infoRevistaAtualizado);

                VisualizarRevistas();

                MensagemColor("\nRevista editada com sucesso!", ConsoleColor.Green);
            }

            Console.ReadLine();
        }

        private void ExcluirCadastroRevista()
        {
            VisualizarRevistas();

            if (ValidaListaVazia(repositorioRevistas.listaDados))
            {
                Revistas idSelecionado; bool idValido;
                do
                {
                    idSelecionado = (Revistas)repositorioRevistas.SelecionarId(ObterIdEscolhido("Digite o ID da Revista que deseja excluir: "));

                    idSelecionado = (Revistas)ValidaId(idSelecionado);

                    idValido = idSelecionado != null;

                } while (!idValido);

                repositorioRevistas.Excluir(idSelecionado);

                VisualizarRevistas();

                MensagemColor("\nRevista excluída com sucesso!", ConsoleColor.Green);
            }

            Console.ReadLine();
        }

        private bool InicializarOpcaoEscolhida()
        {
            string entrada = Console.ReadLine();

            switch (entrada.ToUpper())
            {
                case "1": VisualizarRevistas(); Console.ReadLine(); break;
                case "2": AdicionarCadastroRevista(); break;
                case "3": EditarCadastroRevista(); break;
                case "4": ExcluirCadastroRevista(); break;
                case "S": return false; break;
                default: break;
            }
            return true;
        }

        private Revistas ObterCadastroRevista()
        {
            Revistas infoRevista = new()
            {
                titulo = ObterTitulo(),
                colecao = ObterColecao(),
                edicao = ObterEdicao(),
                ano = ObterAno(),
                caixa = ObterCaixa()
            };

            return infoRevista;
        }

        private string ObterTitulo()
        {
            Console.Write("Escreva o Título da Revista: ");
            string titulo = Console.ReadLine();
            return titulo;
        }

        private string ObterColecao()
        {
            Console.Write("Escreva o Tipo de Coleção: ");
            string colecao = Console.ReadLine();
            return colecao;
        }

        private int ObterEdicao()
        {
            int edicao = ValidaNumero("Escreva o Número da Edição: ");
            return edicao;
        }

        private int ObterAno()
        {
            int ano = ValidaNumero("Escreva o Ano da Revista: ");
            return ano;
        }

        private Caixas ObterCaixa()
        {
            telaCadastroCaixa.VisualizarCaixas();

            if (ValidaListaVazia(repositorioCaixas.listaDados))
            {
                Caixas caixa; bool idValido;
                do
                {
                    caixa = (Caixas)repositorioCaixas.SelecionarId(ObterIdEscolhido("Digite o ID da Caixa que deseja guardar a Revista: "));

                    caixa = (Caixas)ValidaId(caixa);

                    idValido = caixa != null;

                } while (!idValido);

                return caixa;
            }
            Console.ReadLine();
            return null;
        }
    }
}
