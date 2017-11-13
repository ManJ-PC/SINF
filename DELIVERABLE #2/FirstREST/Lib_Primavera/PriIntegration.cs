﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using ADODB;

namespace FirstREST.Lib_Primavera
{
    public class PriIntegration
    {
        const string dateDivisor = "-";
        const string hourDivisor = ":";
        const string dateHourDivisor = " ";


        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {


            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo, CDU_Email as Email FROM  CLIENTES");


                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo"),
                        Email = objList.Valor("Email")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {


            GcpBECliente objCli = new GcpBECliente();


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {

                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    myCli.Morada = objCli.get_Morada();
                    myCli.Email = PriEngine.Engine.Comercial.Clientes.DaValorAtributo(codCliente, "CDU_Email");


                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_Morada(cliente.Morada);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }



        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.Morada);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }



        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------

        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {

            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();
                    myArt.STKAtual = objArtigo.get_StkActual();

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }

        public static List<Model.Artigo> ListaArtigos()
        {

            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");
                    //                  art.STKAtual = objList.Valor("stkatual");


                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo

        #region DocCompra


        public static List<Model.DocCompra> VGR_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }


        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    //PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR,rl);
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        #endregion DocCompra

        #region DocsVenda

        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    System.Diagnostics.Debug.WriteLine("-------------------");
                    System.Diagnostics.Debug.WriteLine(dv.Entidade);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    //PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        System.Diagnostics.Debug.WriteLine(lin.PrecoUnitario);
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    //PriEngine.Engine.Comercial.Vendas.Edita Actualiza(myEnc, "Teste");
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }



        public static List<Model.DocVenda> Encomendas_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT TOP 50 id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' ORDER BY Data DESC");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }


        public static List<Model.DocVenda> Encomendas_List(string entity)
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT TOP 50 id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and Entidade='" + entity + "' ORDER BY Data DESC");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }


        public static Model.DocVenda Encomenda_Get(string numdoc)
        {


            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {


                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }

        #endregion DocsVenda

        #region Agenda

        public static List<string> ListActivities(string representative_id, string month, string year)
        {
            int numberOfDays = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));
            StdBELista objList;
            List<string> listNumActivities = new List<string>();
            for (int i = 0; i < numberOfDays; i++)
                listNumActivities.Add("0");


            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta(
                    "SELECT Day(Tarefas.DataInicio) AS Day, COUNT(Tarefas.Id) AS Count " +
                    "FROM Tarefas, TiposTarefa " +
                    "WHERE Tarefas.IdTipoActividade = TiposTarefa.Id " +
                    "AND Tarefas.Utilizador LIKE '" + representative_id + "' " +
                    "AND Year(Tarefas.DataInicio) = " + year + " " +
                    "AND Month(Tarefas.DataInicio) = " + month + " " +
                    "GROUP BY Day(Tarefas.DataInicio) " +
                    "ORDER BY Day, Count ASC"
                    );

                while (!objList.NoFim())
                {
                    int day = objList.Valor("Day");
                    listNumActivities[day - 1] = objList.Valor("Count").ToString();
                    objList.Seguinte();
                }

                return listNumActivities;
            }
            else
                return null;

        }

        public static List<Model.Activity> ListActivities(string representative_id, string date)
        {
            StdBELista objList;
            List<Model.Activity> listActivities = new List<Model.Activity>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                string[] dateParts = date.Split(new string[] { dateDivisor }, System.StringSplitOptions.None);
                string year = dateParts[0];
                string month = dateParts[1];
                string day = dateParts[2];

                objList = PriEngine.Engine.Consulta(
                    "SELECT Tarefas.Id AS ActivityId, Tarefas.DataInicio, Tarefas.Resumo AS Title, TiposTarefa.Descricao AS Type, Tarefas.EntidadePrincipal AS Client, Tarefas.Utilizador AS RepresentativeId, Tarefas.LocalRealizacao AS Location, Tarefas.Descricao AS Notes " +
                    "FROM Tarefas, TiposTarefa " +
                    "WHERE Tarefas.IdTipoActividade = TiposTarefa.Id " +
                    "AND Tarefas.Utilizador LIKE '" + representative_id + "' " +
                    "AND Year(Tarefas.DataInicio) = " + year + " " +
                    "AND Month(Tarefas.DataInicio) = " + month + " " +
                    "AND Day(Tarefas.DataInicio) = " + day
                    );

                while (!objList.NoFim())
                {
                    DateTime dateTime = objList.Valor("DataInicio");
                    listActivities.Add(new Model.Activity
                    {
                        id = objList.Valor("ActivityId"),
                        date = GetDate(dateTime),
                        hour = GetHour(dateTime),
                        title = objList.Valor("Title"),
                        type = objList.Valor("Type"),
                        client = objList.Valor("Client"),
                        representative_id = objList.Valor("RepresentativeId"),
                        location = objList.Valor("Location"),
                        notes = objList.Valor("Notes")
                    });
                    objList.Seguinte();
                }

                return listActivities;
            }
            else
                return null;

        }

        /*public static Model.Activity GetActivity(string activityId)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                Interop.CrmBE900.CrmBEActividade objActivity = PriEngine.Engine.CRM.Actividades.Edita(activityId);
                if (objActivity == null)
                    return null;
                Model.Activity myActivity = new Model.Activity();
                myActivity.id = objActivity.get_ID();
                myActivity.date = GetDate(objActivity.get_DataInicio());
                myActivity.hour = GetHour(objActivity.get_DataInicio());
                myActivity.title = objActivity.get_Resumo();
                myActivity.type = GetActivityType(objActivity.get_IDTipoActividade());
                myActivity.client = objActivity.get_EntidadePrincipal();
                myActivity.representative_id = objActivity.get_Utilizador();
                myActivity.location = objActivity.get_LocalRealizacao();
                myActivity.notes = objActivity.get_Descricao();
                return myActivity;
            }
            else
                return null;
        }*/

        public static Lib_Primavera.Model.RespostaErro UpdActivity(Lib_Primavera.Model.Activity activity)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            Interop.CrmBE900.CrmBEActividade objActivity = new Interop.CrmBE900.CrmBEActividade();
            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.CRM.Actividades.Existe(activity.id) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "A actividade não existe";
                        return erro;
                    }
                    else
                    {
                        objActivity = PriEngine.Engine.CRM.Actividades.Edita(activity.id);
                        objActivity.set_EmModoEdicao(true);

                        setCrmBEActividadeFields(activity, objActivity);
                        PriEngine.Engine.CRM.Actividades.Actualiza(objActivity);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static Lib_Primavera.Model.RespostaErro DelActivity(string activityId)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            Interop.CrmBE900.CrmBEActividade objActivity = new Interop.CrmBE900.CrmBEActividade();
            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.CRM.Actividades.Existe(activityId) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "A actividade não existe";
                        return erro;
                    }
                    else
                    {
                        PriEngine.Engine.CRM.Actividades.Remove(activityId);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static Lib_Primavera.Model.RespostaErro InsertActivityObj(Model.Activity activity)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            Interop.CrmBE900.CrmBEActividade objActivity = new Interop.CrmBE900.CrmBEActividade();
            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    setCrmBEActividadeFields(activity, objActivity);
                    objActivity = PriEngine.Engine.CRM.Actividades.PreencheDadosRelacionados(objActivity);

                    PriEngine.Engine.CRM.Actividades.Actualiza(objActivity);
                    activity.id = objActivity.get_ID();

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static string GetActivityTypeId(string activityType)
        {
            StdBELista objList;

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta(
                    "SELECT TOP 1 Id " +
                    "FROM TiposTarefa " +
                    "WHERE Descricao LIKE '" + activityType + "'"
                    );

                return objList.Valor("Id");
            }
            else
                return null;
        }

        public static string GetActivityType(string activityTypeId)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                Interop.CrmBE900.CrmBETipoActividade objActivityType = PriEngine.Engine.CRM.TiposActividade.Edita(activityTypeId);
                if (objActivityType != null)
                    return objActivityType.get_Descricao();
                else
                    return null;
            }
            else
                return null;
        }

        private static void setCrmBEActividadeFields(Model.Activity myActivity, Interop.CrmBE900.CrmBEActividade objActivity)
        {
            string dateHour = myActivity.date + dateHourDivisor + myActivity.hour;  // Example: 2017-11-11 16h40
            string[] dateHourStr = dateHour.Split(new Char[] { ' ', '-', ':' });
            int[] date = new int[dateHourStr.Length];
            for (var i = 0; i < dateHourStr.Length; i++)
                date[i] = int.Parse(dateHourStr[i]);

            objActivity.set_DataInicio(new DateTime(date[0], date[1], date[2], date[3], date[4], 0));
            objActivity.set_Resumo(myActivity.title);
            objActivity.set_IDTipoActividade(GetActivityTypeId(myActivity.type));
            objActivity.set_EntidadePrincipal(myActivity.client);
            objActivity.set_Utilizador(myActivity.representative_id);
            objActivity.set_LocalRealizacao(myActivity.location);
            objActivity.set_Descricao(myActivity.notes);
            objActivity.set_DataFim(objActivity.get_DataInicio());
        }

        #endregion Agenda

        #region TargetCustomers

        public static List<Model.TargetCustomer> ListTargetCustomers(string representative_id, string target_customer = null)
        {
            StdBELista objList;

            List<Model.TargetCustomer> listTargetCustomers = new List<Model.TargetCustomer>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta(
                    "SELECT Cliente AS CustomerId, Nome AS Name, Fac_Tel AS PhoneNumber, DataInicio AS Date, Utilizador AS RepresentativeId, LocalRealizacao AS Location " +
                    "FROM Clientes, Tarefas " +
                    "WHERE Clientes.Cliente LIKE Tarefas.EntidadePrincipal " +
                    "AND Utilizador LIKE '" + representative_id + "'" +
                    (target_customer == null ? "" : " AND Cliente LIKE '" + target_customer + "'")
                    );

                while (!objList.NoFim())
                {
                    listTargetCustomers.Add(new Model.TargetCustomer
                    {
                        customer_id = objList.Valor("CustomerId"),
                        name = objList.Valor("Name"),
                        phone_number = objList.Valor("PhoneNumber"),
                        date = GetDateWithHour(objList.Valor("Date")),
                        location = objList.Valor("Location")
                    });
                    objList.Seguinte();

                }

                return listTargetCustomers;
            }
            else
                return null;
        }

        #endregion

        #region Opportunities

        public static List<Model.Opportunity> ListOpportunities(string representativeId)
        {
            StdBELista objList;

            List<Model.Opportunity> listOpportunities = new List<Model.Opportunity>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta(
                    "SELECT CabecOportunidadesVenda.ID AS OpportunityId, CabecOportunidadesVenda.Entidade AS CustomerId, Clientes.Nome AS CustomerName, ProdutosATP.IdProduto AS ProductId, ProdutosATP.Descricao AS ProductName, Tarefas.Resumo AS OpportunityType, Vendedores.Vendedor AS RepresentativeId " +
                    "FROM CabecOportunidadesVenda, Clientes, ProdutosATP, Tarefas, Vendedores " +
                    "WHERE CabecOportunidadesVenda.Entidade LIKE Clientes.Cliente " +
                    "AND Clientes.Cliente LIKE Vendedores.Vendedor " +
                    "AND Tarefas.Utilizador LIKE Vendedores.Vendedor"
                    );

                while (!objList.NoFim())
                {
                    listOpportunities.Add(new Model.Opportunity
                    {
                        opportunity_id = objList.Valor("OpportunityId"),
                        customer_id = objList.Valor("CustomerId"),
                        customer_name = objList.Valor("CustomerName"),
                        product_id = objList.Valor("ProductId"),
                        product_name = objList.Valor("ProductName"),
                        opportunity_type = objList.Valor("OpportunityType"),
                        representative_id = objList.Valor("RepresentativeId")
                    });
                    objList.Seguinte();
                }

                return listOpportunities;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdOpportunity(Lib_Primavera.Model.Opportunity opportunity)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            Interop.CrmBE900.CrmBEOportunidadeVenda objOpportunity = new Interop.CrmBE900.CrmBEOportunidadeVenda();
            try
            {

                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.CRM.OportunidadesVenda.Existe(opportunity.opportunity_id) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "A oportunidade não existe";
                        return erro;
                    }
                    else
                    {
                        objOpportunity = PriEngine.Engine.CRM.OportunidadesVenda.Edita(opportunity.opportunity_id);
                        objOpportunity.set_EmModoEdicao(true);

                        objOpportunity.set_Vendedor(opportunity.representative_id);
                        objOpportunity.set_Descricao(opportunity.opportunity_type);
                        objOpportunity.set_Entidade(opportunity.customer_id);
                        objOpportunity.set_Resumo(opportunity.product_id);

                        PriEngine.Engine.CRM.OportunidadesVenda.Actualiza(objOpportunity);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.RespostaErro DelOpportunity(string opportunityId)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            Interop.CrmBE900.CrmBEOportunidadeVenda objOpportunity = new Interop.CrmBE900.CrmBEOportunidadeVenda();
            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.CRM.OportunidadesVenda.Existe(opportunityId) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "A oportunidade não existe";
                        return erro;
                    }
                    else
                    {
                        PriEngine.Engine.CRM.OportunidadesVenda.Remove(opportunityId);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static Lib_Primavera.Model.RespostaErro InsertOpportunityObj(Model.Opportunity opportunity)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            Interop.CrmBE900.CrmBEOportunidadeVenda objOpportunity = new Interop.CrmBE900.CrmBEOportunidadeVenda();
            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    objOpportunity.set_ID(Guid.NewGuid().ToString());
                    objOpportunity.set_IDCabecInterno(objOpportunity.get_ID());
                    objOpportunity.set_Vendedor(opportunity.representative_id);
                    objOpportunity.set_Descricao(opportunity.opportunity_type);
                    objOpportunity.set_Entidade(opportunity.customer_id);
                    objOpportunity.set_Resumo(opportunity.product_id);
                    objOpportunity.set_TipoEntidade("C");
                    objOpportunity.set_CicloVenda("CV_HW");
                    objOpportunity.set_CriadoPor(opportunity.representative_id);
                    objOpportunity.set_DataCriacao(DateTime.Now);
                    objOpportunity.set_DataExpiracao(new DateTime(5000, 1, 1));
                    objOpportunity.set_Moeda("EUR");

                    PriEngine.Engine.CRM.OportunidadesVenda.Actualiza(objOpportunity);
                    opportunity.opportunity_id = objOpportunity.get_ID();

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }
            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        #endregion

        #region CommonFunctions

        private static string GetDate(DateTime dateTime)
        {
            return dateTime.Year + dateDivisor + dateTime.Month + dateDivisor + dateTime.Day;
        }

        private static string GetHour(DateTime dateTime)
        {
            return dateTime.Hour + hourDivisor + dateTime.Minute;
        }

        private static string GetDateWithHour(DateTime dateTime)
        {
            return GetDate(dateTime) + dateHourDivisor + GetHour(dateTime);
        }

        #endregion
    }
}
