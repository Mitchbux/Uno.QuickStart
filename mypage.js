

  my own [filter] { filter += eval((this.toString())); },

  my ("once"), 
  my [collection] {collection = "<node>" + js.my.own.filter() + "</node>"; },

  
  
  # Module template example --
  module [book] {<br/><a 
    class="button is-info" 
    style="font-size: 9pt;"  
    href="${code}"> 
    <fontawesome><i class="fas fa-globe"></i></fontawesome>&nbsp;
     ${name} 
    </a> <br/>

    ${js.my.own.add("js.my.book['"+name+"']") ? "" : ""}
  },

  
  my book 
  [MP3] { https://mitchbux.github.io/ok.github.io/DrOkomode.mp3 }
  [Editor] { https://spck.io/dark }
  [Delivered] { https://urlz.fr/duyI }
  [Code] { https://repl.it/@DanyCase/Delivered#index.html }
  [Playtime] { https://mitchbux.github.io/ }
  [Twitter] { https://twitter.com/gotocaca },
  


  str [toc] { :: bookmarks :: },
  json [footer] { label : " :::/ by Mitchbux /::: ", line : "<br/>" } ,
  
  ## Global stack
  {
    return js.str.toc + js.my.collection() 
    + js.footer.line + js.footer.label + js.footer.line;   
  }
  
  