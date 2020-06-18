str [msg] {
    This is multiline.
    Zen up world. 
},"once :"  

[download] { download = "fromCloud : " + stack; }

[css]{
    body {background-color:#202020;color:#fff;}
    span {color:#eee;}
}

body html [span] {
    Hello,<br/>
    <b><pre>${js.str.msg}</pre></b>
}

body html span [p] {
    <div class="footer">Feel free to use and distribute</div>
}

body html [span] p div.footer { ${js.download('MyPage')} } 
[onclick] {

        if (!js.counter) 
            js.counter = 0;

    alert("You clicked " + (++js.counter) + " time(s)");

    js.str.msg += "Click here to rewrite click.\\n";

}

[onerror] { return "Something went wrong."; }

