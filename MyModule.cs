//2015, MIT, EngineKit

using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
//using System.Net;

using SharpConnect.WebServers;
namespace SharpConnect
{
    enum ConvPlan
    {
        Unknown,
        ToString,
        ToDouble,
        ToInt32,
        ToFloat,
        ToShort
    }

    class ParameterAdapter
    {
        public string ParName;
        public Type ParType;
        public ConvPlan convPlan;

        public ParameterAdapter(string parName, Type parType)
        {
            //1. Convert
            //2. 

            //3. 
            //try
            //{
            //    double dd = double.Parse("A");

            //    double x;
            //    if (double.TryParse("A", out x))
            //    {

            //    }

            //    if (double.TryParse("A", out double result))
            //    {

            //    }


            //}
            //catch (Exception ex)
            //{

            //}



            ParName = parName;
            ParType = parType;
            //convert from string to target type
            switch (ParType.FullName)
            {
                default:
                    break;
                case "System.Int32":
                    convPlan = ConvPlan.ToInt32;
                    break;
                case "Systm.String":
                    convPlan = ConvPlan.ToString;
                    break;
                case "System.Double":
                    convPlan = ConvPlan.ToDouble;
                    break;
            }
        }
        public object GetActualValue(string strValue)
        {
			try
			{
				switch (convPlan)
				{
					default:
						return null;
					case ConvPlan.ToDouble:
						return double.Parse(strValue); //
					case ConvPlan.ToFloat:
						return float.Parse(strValue);
					case ConvPlan.ToInt32:
						return int.Parse(strValue);
				}
			}
			catch {
				return strValue;
			}
        }
        public override string ToString()
        {
            return ParType + " " + ParName;
        }
    }
    class MetAdapter
    {
        public Object moduleInstance;
        public MethodInfo metInfo;
        bool _conservativeForm;

        ParameterAdapter[] parAdapters;
        public MetAdapter(Object moduleInstance, MethodInfo metInfo)
        {
            this.moduleInstance = moduleInstance;
            this.metInfo = metInfo;

            //conservative form
            ParameterInfo[] pars = metInfo.GetParameters();
            if (pars.Length == 2 &&
                pars[0].ParameterType == typeof(HttpRequest) &&
                pars[1].ParameterType == typeof(HttpResponse))
            {
                _conservativeForm = true;
            }
            else
            {
                parAdapters = new ParameterAdapter[pars.Length];

                int i = 0;
                foreach (ParameterInfo p in pars)
                {
                    ParameterAdapter parAdapter = new ParameterAdapter(p.Name, p.ParameterType);
                    parAdapters[i] = parAdapter;
                    i++;
                }
                _conservativeForm = false;
            }
        }
        object[] PrepareInputArgs(HttpRequest req)
        {

            int j = parAdapters.Length;
            object[] pars = new object[j];
            for (int i = 0; i < j; ++i)
            {
                ParameterAdapter a = parAdapters[i];
                string inputValue = req.GetReqParameterValue(a.ParName);


                pars[i] = a.GetActualValue(inputValue);
            }
            return pars;
        }

        public void Invoke(HttpRequest req, HttpResponse resp)
        {
			if (_conservativeForm)
			{
				this.metInfo.Invoke(
					moduleInstance, new object[] { req, resp }
				);
			}
			else
			{
				try
				{
					//....prepare input 
					object result = metInfo.Invoke(moduleInstance, PrepareInputArgs(req));
					//prepare result
					resp.End(result.ToString());
				}
				catch
				{
					resp.End("Error, Please Try Again");
				}
			}
        }
    }

    class UserUnHxListEventArgs : EventArgs
    {
        //...
		public string DataJSON;
		public List<int> MoveCountRedo;

	}
	//3.
	class MyModule
    {
        //public EventHandler<UserUnHxListEventArgs> DataArrived1;
        public event EventHandler<UserUnHxListEventArgs> DataArrived;

        //4.
        [HttpMethod]
        public void Go(HttpRequest req, HttpResponse resp)
        {
            string content = req.GetBodyContentAsString();
            //json content
            //convert to undo hx list
            //
            UserUnHxListEventArgs evArgs = new UserUnHxListEventArgs();
            if (DataArrived != null)
            {
                evArgs.MoveCountRedo = null;//TODO: change to history list
                DataArrived(this, evArgs);
            }

            resp.End("go!");
        }
        //4.
        [HttpMethod(AlternativeName = "mysay")]
        [HttpMethod(AlternativeName = "mysay1")]
        public void Say(HttpRequest req, HttpResponse resp)
        {
            resp.End("say!12345");
        }

	}
    class MyModule2
    {
        //4.
        [HttpMethod]
        public void Walk(HttpRequest req, HttpResponse resp)
        {
            resp.End("walk!");
        }
        //4.
        [HttpMethod(AlternativeName = "mysay")]
        [HttpMethod(AlternativeName = "mysay1")]
        public void Fly(HttpRequest req, HttpResponse resp)
        {
            resp.End("fly");
        }
    }

    class MyModule3
    {
        //4.
        [HttpMethod]
        public void Go1(HttpRequest req, HttpResponse resp)
        {
            resp.End("go1!");
        }
        //4.
        [HttpMethod(AlternativeName = "mysay")]
        [HttpMethod(AlternativeName = "mysay1")]
        public void Say1(HttpRequest req, HttpResponse resp)
        {
            resp.End("say1!7890");
        }
    }
    //5. 
    class MyAdvanceMathModule
    {
        [HttpMethod]
        public void CalculateSomething1(HttpRequest req, HttpResponse resp)
        {
            string s1_s = req.GetReqParameterValue("s1");
            string s2_s = req.GetReqParameterValue("s2");
            //..... 
            double result = CalculateSomething(double.Parse(s1_s), double.Parse(s2_s));

            //.....
            resp.End(result.ToString());

        }
        [HttpMethod]
        public double CalculateSomething(double s1, double s2)
        {
            return s1 + s2;
        }

        [HttpMethod]
        public double CalculateX(double s1, double s2)
        {
            return s1 * s2;
        }
    }
    class MMath1
    {

        [HttpMethod]
        public double CalculateSomething(double s1, double s2)
        {
            return s1 + s2;
        }

        [HttpMethod]
        public double CalculateX(double s1, double s2)
        {
            return s1 * s2;
        }
        [HttpMethod]
        public string RegisterNewUser(string username)
        {
            return "hello " + username;
        }
    }
	class JSONLoad
	{
		//public EventHandler<UserUnHxListEventArgs> DataArrived1;
		public event EventHandler<UserUnHxListEventArgs> DataArrived3;
		[HttpMethod]
		public void SavePanel(HttpRequest req, HttpResponse resp)
		{
			string content = req.GetBodyContentAsString();
			
			UserUnHxListEventArgs evArgs = new UserUnHxListEventArgs();
			if (DataArrived3 != null)
			{
				evArgs.DataJSON = content;
				//evArgs.MoveCountRedo = null;//TODO: change to history list
				DataArrived3(this, evArgs);
			}
			resp.End("Success");
			
			//string s = content;

			//UserUnHxListEventArgs evArgs = new UserUnHxListEventArgs();
			//if (content != null)
			//{
			//	string[] data = s.Split('|');
			//	if (data.Length == 3)
			//	{
			//		string savePanels1 = data[0];
			//		string historyPanels1 = data[2];
			//		string listCountHistory1 = data[1];
			//		savePanels = JsonConvert.DeserializeObject<List<MyControl1.TargetJSON>>(data[0]);
			//		historyPanels = JsonConvert.DeserializeObject<List<MyControl1.TargetUndo>>(data[2]);
			//		listCountHistory = JsonConvert.DeserializeObject<List<int>>(data[1]);

			//		resp.End(savePanels1);
			//		resp.End(historyPanels1);
			//		resp.End(listCountHistory1);
			//		//evArgs.RedoList = data[0];//TODO: change to history list
			//		//DataArrived(this, evArgs);
			//	}
			//resp.End(savePanels);
		}
	}

		abstract class A
    {
        public abstract void Test();
        public abstract void Test1();
        public abstract void Test2();
        public abstract void Test3();
        public abstract void Test4();
        public abstract void Test5();
    }
    class B : A
    {
        public override void Test()
        {
            //.... actions...
        }
        public override void Test1()
        {
            throw new NotImplementedException();
        }
        public override void Test2()
        {
            throw new NotImplementedException();
        }

        public override void Test3()
        {
            throw new NotImplementedException();
        }

        public override void Test4()
        {
            throw new NotImplementedException();
        }

        public override void Test5()
        {
            throw new NotImplementedException();
        }
    }
}