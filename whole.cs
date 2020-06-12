using System; using System.IO; using System.Text; using System.Collections; using System.Collections.Generic; using System.Linq; using System.Net; using System.Web; using System.Dynamic;using System.Reflection; using Microsoft.CSharp; using System.CodeDom.Compiler;

namespace TwoSine{
    
    public delegate string load(string added);
    public delegate void indexa(string name, string code);
    public delegate void filter(string those, string stack, ref String result);

public class Wise : DynamicObject
{
    
    Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
    List<string> list = new List<string>();


    private load reloading;
    private indexa indexing;
    private filter filtering;

    public Wise()
    {
        reloading = defaultLoader;
        indexing = defaultIndexer;
        filtering = defaultFilter;
    }

    public Wise(string[] stack){
        reloading = defaultLoader;
        indexing = defaultIndexer;
        filtering = defaultFilter;

        list.AddRange(stack);
    }

    public Wise(object stack){

        reloading = defaultLoader;
        indexing = defaultIndexer;
        filtering = defaultFilter;

            Type type = stack.GetType();
            FieldInfo[] field = type.GetFields();
            PropertyInfo[] myPropertyInfo = type.GetProperties();

            String value = null;

        foreach (var propertyInfo in myPropertyInfo)
        {
            if (propertyInfo.GetIndexParameters().Length == 0)
            {
                value = propertyInfo.GetValue(stack, null) as String;
                dictionary.Add(propertyInfo.Name.ToString(), value);
            }
        }
    }

    public bool hasOwnProperty(string property)
    {
        return dictionary.ContainsKey(property);
    }

    public void setLoading(string code){
        //todo: compile
        reloading = defaultLoader;
    }

    public void setIndexing(string code){
        //todo: compile
        filtering = defaultFilter;
    }

    private string defaultLoader(string added) {
        return added;
    }

    private void defaultFilter(string those, string stack, ref String result) 
    {
            string filterName = result + "";

        if (filterName!="") filterName += ",";

        filterName += those;

        result = filterName;
    }

    private void defaultIndexer(string name, string code) 
    {
        load newFilter = (stack) => {
                
                string result = "";

            foreach(string those in list){ 
                filtering(those, stack, ref result); }

            return result;

        };

        dictionary[name] = newFilter;
    }


    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        return dictionary.TryGetValue(binder.Name.ToLower(), out result);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        dictionary[binder.Name.ToLower()] = value;return true;
    }

    public override bool TryGetIndex (System.Dynamic.GetIndexBinder binder, object[] indexes, out object result)
    {
            int index = (int)indexes[0];

        try
        {
            result = list[index];
            return true;

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        result = null;
        return false;
    }

    public override bool TrySetIndex (System.Dynamic.SetIndexBinder binder, object[] indexes, object value)
    {
            int index = (int)indexes[0];

        try
        {
            list[index] = "" +(value);
            return true;

        }catch(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public int index{
        get; set;
    }

    public void add(string added){
        list.Add(reloading(added));
    }

    public string first
    {
        get
        {
            index = 0;
            return list[index];
        }

        set
        {   
            index = 0;
            list[index] = value;
        }
    }

    public string next
    {
        get
        {
            index++;
            if (index==list.Count)
                index=list.Count-1;
            return list[index];
        }

        set
        {   
            index++;
            if (index==list.Count)
                index=list.Count-1;
            list[index] = value;
        }

    }

    public string last
    {
        get
        {
            index = list.Count-1;
            return list[index];
        }

        set
        {   
            index = list.Count-1;
            list[index] = value;
        }
    }

    public string previous
    {
        get
        {
            if (index<=0) index=1;
            index--;
            return list[index];
        }

        set
        {   
            if (index<=0) index=1;
            index--;
            list[index] = value;
        }

    }

    // TODO : PLUS - MINUS
    public int pus=-1;
    public int mus=1;

    public void plus(){

    }

    public void minus(){

    }
    // TODO



    public override string ToString()
    {
        return String.Join(",",list.ToArray());
    }

    //TODO
    public string getter()
    {
        return "";
    }

    //TODO
    public string setter(string stack)
    {
        return "";
    }

    public string stack
    {
        get{
            return getter();
        }
        set{
            setter(value);
        }
    }

    public void indexer(string name, string code)
    {
        indexing(name, code);
    }

    public void loader(string name, string code)
    {
    
            Wise newLoader = dictionary[name] as Wise;
        if (newLoader==null) newLoader = new Wise();

        newLoader.setLoading(code);
        dictionary[name] = newLoader;
    }

    public void module(string name, string code)
    {
        
            Wise newModule = dictionary[name] as Wise;
        if (newModule==null) newModule = new Wise();

        newModule.setIndexing(code);
        dictionary[name] = newModule;
    }

    //TODO
    public void WON(string script)
    {

    }

    //TODO
    public void JSON(string script)
    {

    }

    //TODO
    public void XML(string script)
    {

    }

}

    public static class Strings{

        public static string empty = "";

        public static string after(this string s, string a)
        {
            if (s.IndexOf(a)>-1) return s.Substring(s.IndexOf(a) + a.Length);
            else return Strings.empty;
        }

        public static string before(this string s, string b)
        {
            if (s.IndexOf(b)>-1) return s.Substring(0, s.IndexOf(b)); 
            else return Strings.empty;
        }

        public static string replace(this string s, string a, string b)
        {
            return String.Join(b, s.Split(a));
        }

    }

    public class Whole{
        
        public static dynamic cs = new Wise();
        private static CSharpCodeProvider provider = new CSharpCodeProvider();
        private static CompilerParameters parameters = new CompilerParameters();


        public static object evalObject(string code, string stack="")
        {
            Type codeType;
            code = "using System; using System.IO; using TwoSine; using System.Text; using System.Collections; using System.Collections.Generic; using System.Linq; using System.Web; using System.Dynamic;using System.Reflection; using Microsoft.CSharp;\n"+
                " namespace TwoSine{ public class Code{public static Object Block(dynamic cs, string stack){ "+code+" return \"\"; }}} ";
            return TryCompilerResults(code, "Block", out codeType).Invoke(codeType, new object[]{cs, stack});
        }

        public static object evalLoad(string code, dynamic those=null)
        {
            Type codeType;
            code = "using System; using System.IO; using TwoSine; using System.Text; using System.Collections; using System.Collections.Generic; using System.Linq; using System.Web; using System.Dynamic;using System.Reflection; using Microsoft.CSharp;\n"+
                " namespace TwoSine{ public delegate string load(string added); public class Code{public static TwoSine.load Load(dynamic cs, dynamic those){ "+code+" return null; }}} ";
            return TryCompilerResults(code, "Load", out codeType).Invoke(codeType, new object[]{cs, those});
        }

        public static object evalIndexa(string code, dynamic those=null)
        {
            Type codeType;
            code = "using System; using System.IO; using TwoSine; using System.Text; using System.Collections; using System.Collections.Generic; using System.Linq; using System.Web; using System.Dynamic;using System.Reflection; using Microsoft.CSharp;\n"+
                " namespace TwoSine{ public delegate void indexa(string name, string code);public class Code{public static TwoSine.indexa Index(dynamic cs, dynamic those){ "+code+" return null; }}} ";
            return TryCompilerResults(code, "Index", out codeType).Invoke(codeType, new object[]{cs, those});
        }

        public static object evalFilter(string code, dynamic those=null)
        {
            Type codeType;
            code = "using System; using System.IO; using TwoSine; using System.Text; using System.Collections; using System.Collections.Generic; using System.Linq; using System.Web; using System.Dynamic;using System.Reflection; using Microsoft.CSharp;\n"+
                " namespace TwoSine{ public delegate void filter(string those, string stack, ref String result); public class Code{public static TwoSine.filter Filter(dynamic cs, dynamic those){ "+code+" return null; }}} ";
            return TryCompilerResults(code, "Filter", out codeType).Invoke(codeType, new object[]{cs, those});
        }

        public static MethodInfo TryCompilerResults(string code, string method, out Type codeType)
        {
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Compile [{0}] {1}", error.ErrorNumber, error.ErrorText));
                }
                throw new InvalidOperationException(sb.ToString());
            }
            

            Assembly assembly = results.CompiledAssembly;
            codeType = assembly.GetType("TwoSine.Code");
            MethodInfo block = codeType.GetMethod(method);
            return block;
        }

        public static void CreateReferences()
        {
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Web.dll");
            parameters.ReferencedAssemblies.Add("System.Net.dll");
        }

        public static void Main(string [] arguments)
        {
            CreateReferences();

            foreach(string s in arguments)
            {
                try
                {
                    string script = Encoding.UTF8.GetString(File.ReadAllBytes(s));
                    

                    //cs.loader("load", "Encoding.UTF8.GetString(File.ReadAllBytes(added));");
                    //cs.module("write","(name, code) => { File.WriteAllBytes(name, Encoding.UTF8.GetBytes(code));}");
                    //cs.module("str","(name, code) => {this.name = code;} ");
                    //cs.module("cs","(name, code) => {cs.name = eval(code);} ");
                    evalLoad("cs.add(\"Hello\");");
                    evalLoad("cs.add(\"World\");");
                    cs.WON(script);                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    if (cs.hasOwnProperty("onerror"))
                        cs.onerror(ex);
                }

            }

            try
            {
                Console.WriteLine(cs);
                Console.WriteLine(cs.first);
                Console.WriteLine(cs.next);
                Console.WriteLine(cs.last);
                Console.WriteLine(cs.previous);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                if (cs.hasOwnProperty("onerror"))
                    cs["onerror"](ex);
            }
        }
    }

}
/*
Array.prototype.plus = function(){if (!this[-1]){this[-1] = [,[]];}else this.pus++;return this[this.mus][this.pus]=[];}
Array.prototype.minus = function(){if (!this[-1]){this[-1] = [,[]];}else this.mus--; this.pus=0;return this[this.mus]=[];}
Array.prototype.mus = -1;Array.prototype.pus = 1;


//parse Wise Object Notation
var swon = {text:"", char:0, stack:"", until:function(a,b){var level=0; var result = "";
for(this.char++;this.char<this.text.length;this.char++){  if ((this.here+"")==b){if (level<=0) return result; else level--;}  result += this.here; if ((this.here+"")==a){level++;}}},
name: "", root: "js", node: "js", exist:{}, rootStack:["js"]};
Object.defineProperty(swon, "here", {get:function(){return this.text[this.char];}});

var bwon= [function(){swon.rootStack.push(swon.root); swon.root = swon.node;  return "";}, function(){swon.root = swon.rootStack.pop(); return "";}, function(){swon.node = swon.root; swon.name = ""; return "";}
, function(){var code = swon.until("{","}");code="function(stack){"+code+"}";return swon.node +".getter="+code+";"}, function(){return "";}
, function(){var q="`"; if ((swon.node.indexOf("module")>-1) || (swon.node.indexOf("loader")>-1)){q="'";} var idx = swon.until("", "]"); var skip = swon.until("","{"); var cde = swon.until("{","}"); return swon.node + ".indexer("+doubleq(idx) + "," + q + cde + q + ");"; }, function(){return "";}
, function(){var result = swon.node +".cell="+swon.node+".plus();"; swon.node += ".cell"; return result;}, function(){swon.stack = "value"; return "";}
, function(){var result = swon.node +".cell="+swon.node+".minus();"; swon.node += ".cell"; return result;}, function(){return "";}
, function(){var data = "`" + swon.until("", "'") + "`"; return swon.node + ".add(" + data + ");";}, function(){var data = "`" +swon.until("", "\"")+ "`"; return swon.node + ".add(" + data + ");";}];
bwon.token= "(),{}[]+=-*'\"#";bwon.breaker= "(),{}[]+=-*'\"# \t\r\n";
bwon[-1] = function(){if (swon.stack=="")return ""; swon.name=swon.stack;if (swon.exist[swon.node + "." + swon.name] || swon.name=="module" || swon.name=="loader"){ swon.node += "."+swon.name; return ""; }else {swon.node += "."+swon.name; swon.exist[swon.node]={};return swon.node+"=[];";} };
var comment = bwon.token.indexOf("#");var quote = function(str){return "'" + str +"'";};var doubleq = function(str){return "\"" + str +"\"";};bwon[comment] = function(){var skip=swon.until("", "\n"); return "";};

Array.prototype.WON = function(wonstr){swon.text=wonstr; var result=""; var ok="";
for(swon.char=0;swon.char<swon.text.length;swon.char++){
  if (bwon.breaker.indexOf(swon.here)>-1){
   if (swon.stack.length>0){result+=bwon[-1](); }
    swon.stack=""; result+=bwon[bwon.token.indexOf(swon.here)](); 
    }else swon.stack += swon.here;}  eval(result) };

*/