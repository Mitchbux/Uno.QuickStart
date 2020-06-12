
str [msg] {
    Hello World
}
,

"once :"  [CurrentDir] { CurrentDir=Directory.GetCurrentDirectory(); } 
,

{ 
    Console.WriteLine(cs.CurrentDir());
        return cs.str.msg;  
}