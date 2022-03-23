import { InteractiveChatbox } from "./chat.js";
import { bot } from "./ankibot.js";
import { card } from "./card.js";
import { Student } from "./login.js";
export var ankete=[];

/*ucitaj box za bota*/
export function box(host) 
{
    const chatButton = host.querySelector('.chatbox__button');
    const chatContent = host.querySelector('.chatbox__support');
    const icons = {
        isClicked: '<img src="/images/icons/chatbox-icon.svg" />',
        isNotClicked: '<img src="/images/icons/chatbox-icon.svg" />'
    }
    const chatbox = new InteractiveChatbox(chatButton, chatContent, icons);
    chatbox.display();
    chatbox.toggleIcon(false, chatButton);
}
let zaposleni = document.createElement("button");
let lokacija = document.createElement("button");
let organizacija = document.createElement("button");

 /*napravi formu*/
var Login=true
export function introduce(host,FF)
{
    
    let container2 = document.createElement('div');
    container2.classList.add(".containerQ"); //ovo ko da nije 
    host.appendChild(container2);
        let home = document.createElement('div');
        home.classList.add(".flex-column");
        home.classList.add("flex-center")
        container2.appendChild(home);
        let h1 = document.createElement("h1");
        h1.classList.add("h1");
        console.log(Student);
        h1.appendChild(document.createTextNode("Dobrodosli  "+Student.ime+" "+Student.prezime+"!"));
        home.appendChild(h1);
        
        zaposleni.classList.add("btnq");
        zaposleni.innerHTML="Zaposleni"
        home.appendChild(zaposleni);
        zaposleni.classList.add("bouncy")
    
        lokacija.classList.add("highscore-btn");
        lokacija.classList.add("btnq");
        lokacija.classList.add("bouncy");
        lokacija.innerHTML="Lokacija";
        home.appendChild(lokacija);
    
        organizacija.classList.add("btnq");
        organizacija.innerHTML="Organizacija";
        organizacija.classList.add("bouncy")
        home.appendChild(organizacija);

        Login==true?PreuzmiAnkete():Finished() //bugg,opet vucem
        function PreuzmiAnkete(){
        fetch("https://localhost:5001/Popunjavanje/PreuzmiNePopunjeneAnkete/"+Student.id,{ method:"GET"})
            .then(s=>{
                if(s.ok){
                    s.json().then(anketeObj=>{
                        if(anketeObj===null) alert("Popunili ste sve ankete! Hvala!");
                        else{
                            anketeObj.forEach(anketaObj=>{
                                ankete.push(anketaObj);
                                }
                            )
                        }
                        Finished();
                    })
                }
            })
        }

        function Finished(){  
            ankete.forEach(anketa=>{  
                if(anketa.entitet===0) {zaposleni.setAttribute("data-anketaID",anketa.id);zaposleni.setAttribute("data-Entitet","0")}
                if(anketa.entitet===1) {lokacija.setAttribute("data-anketaID",anketa.id);lokacija.setAttribute("data-Entitet","1")}
                if(anketa.entitet===2) {organizacija.setAttribute("data-anketaID",anketa.id);organizacija.setAttribute("data-Entitet","2")}
            })
            for(let Entitet=0;Entitet<=2;Entitet++){
                let Uradjena=true;
                ankete.forEach(anketa=>{
                    if(anketa.entitet === Entitet) Uradjena=false;
                })
                if(Uradjena){
                    switch(Entitet){
                        case 0: Disable(zaposleni); break;
                        case 1: Disable(lokacija); break;
                        case 2: Disable(organizacija); break;
                    }
                }
            }
        }

        zaposleni.addEventListener("click",e=>choose(host,zaposleni))
        lokacija.addEventListener("click",e=>choose(host,lokacija))
        organizacija.addEventListener("click",e=>choose(host,organizacija))

        assistant(host);
}
function Disable(button){  
    button.innerHTML+=" ✔️"
    button.style.opacity="0.2";
    button.style.pointerEvents="none";
}
let div2,div25
function choose(host,Anketa)
{       
    host.classList.add("moje");
    host.removeChild(host.firstChild);
    let div1 = document.createElement('div');
    div1.classList.add("container");
    host.prepend(div1);
    
    div2 = document.createElement("div"); //odavde
    const newLocal = "game"; //.???
    div2.classList.add(newLocal);
    div2.classList.add("justify-center");
    div2.classList.add("flex-column"); //msm da nece da radi ovo kao sto nije radilo za body
    div1.appendChild(div2);

    div25= document.createElement("div"); //ovaj ne
    div25.classList.add("hud");
    div2.appendChild(div25);

    let div3 = document.createElement("div");
    div3.classList.add("hud-item");
    div25.append(div3);

    let p = document.createElement("p");
    p.classList.add("hud-prefix");
    p.classList.add("progressText");
    p.innerHTML = "Question";
    div3.appendChild(p);
    
    let div4 = document.createElement("div");
    div4.classList.add("progressBar");
    div3.appendChild(div4);

    let div5 = document.createElement("div");
    div5.classList.add("progressBarFull");
    div4.appendChild(div5);
    
    let div23=document.createElement("div");
    div25.appendChild(div23);
    div23.classList.add("hud-prefix");

    let NextDugme=document.createElement("button");
    div23.appendChild(NextDugme);
    NextDugme.classList.add("buttonNext");
    let span=document.createElement("span");
    NextDugme.appendChild(span);
    span.innerText="Preskocite";

    NextDugme.addEventListener('click',e=>NextQuestion())

    let questionCounter = -1;;
    let questions=[];
    let MAX_QUESTIONS=0; 
    let answers=[],comments=[];
    console.log(Anketa.getAttribute("data-Entitet"))

    fetch("https://localhost:5001/Pitanje/PreuzmiPitanja/"+Anketa.getAttribute("data-Entitet"),
    {
        method: "GET"
    })
    .then(p=>{
        if(p.ok){
            p.json().then(pitanja=>{
                if(pitanja===null)
                    alert("nema pitanja");
                pitanja.forEach(pitanje=>{
                    questions.push(pitanje);
                    MAX_QUESTIONS++;
                })
                window.parent.ShowQuestions();
            })
        }
    })
    
    window.ShowQuestions=function(){
            
        questionCounter++;
        const progressBarFull = host.querySelector(".progressBarFull");
        const progressText = host.querySelector(".progressText");
        progressText.innerText=`Question ${questionCounter + 1} of ${MAX_QUESTIONS}`;
        let acceptingAnswers = true;
        let pom=100*(questionCounter+1)/MAX_QUESTIONS;
        progressBarFull.style.width=pom+"%";
        
        let div6 = document.createElement('h1');
        div6.classList.add("question");
        let PitanjeObj=questions[questionCounter];
        if(typeof PitanjeObj !== 'undefined'){  //kraj ankete?
            div6.innerHTML = PitanjeObj.tekst_pitanja; 
            div2.appendChild(div6);
            let TypeQuestion = PitanjeObj.tip_pitanja;
            
            if(TypeQuestion === 0){ //biraj
                let BrojOdgovora=0;
                let odgovori=PitanjeObj.moguci_odgovori;
                for(let j=0;j<odgovori.length;j++)
                    if(odgovori[j]==='.') 
                        BrojOdgovora++;
                odgovori=odgovori.split(".");
                let k;
                for(k=0;k<BrojOdgovora;k++)
                {
                    let div7 = document.createElement("div");
                    div7.classList.add("choice-container");
                    div2.appendChild(div7);
                    let p1 = document.createElement("div");
                    p1.classList.add("choice-prefix");
                    p1.innerHTML = k+1+")";
                    div7.appendChild(p1);
                    
                    let p2 = document.createElement("div");
                    p2.classList.add("choice-text")
                    p2.innerHTML = odgovori[k];
                    p2.setAttribute("data-number",k+1);
                    div7.appendChild(p2);
                }
                const choices = Array.from(host.querySelectorAll('.choice-text'));

                choices.forEach(choice => {
                    choice.addEventListener('click',e=>{
                        if(!acceptingAnswers) return;

                        acceptingAnswers=false;
                        const selectedChoice = e.target;
                        const selectedAnswer = selectedChoice.dataset["number"]; //ovako vucem broj?
                        selectedChoice.parentElement.classList.add("correct");

                        setTimeout(()=>{
                            selectedChoice.parentElement.classList.remove("correct");
                            NextQuestion()
                        },1500)
                        
                        answers[questionCounter]=selectedChoice.innerText; //valjda ce da upise odgovor
                        //answers[questionCounter]="samo radi";
                        //comments[questionCounter]="samo radi";
      
                    })
                })
            }
            else if(TypeQuestion === 1){                       //OCENI
                let containerS=document.createElement('div');
                containerS.classList.add("containerS");
                div2.appendChild(containerS);
                
                let post=document.createElement('div');
                post.classList.add("post");
                containerS.appendChild(post);

                let tekstic=document.createElement('div');
                tekstic.classList.add("text");
                let edit=document.createElement('div');
                edit.classList.add("edit");
                post.appendChild(tekstic);
                post.appendChild(edit);
                tekstic.innerHTML="Hvala na komentaru!";
                edit.innerHTML="EDIT";
            
                let star_widget=document.createElement('div');
                star_widget.classList.add("star-widget");
                containerS.appendChild(star_widget);

                for(let i=5;i>=1;i--){
                    var radio=document.createElement('input');
                    radio.setAttribute("type","radio");
                    radio.setAttribute("id",'rate-'+i);
                    radio.setAttribute("name","rate");
                    radio.setAttribute("data-number",i);
                    star_widget.appendChild(radio);
                    
                    let label=document.createElement("label");
                    label.setAttribute("for","rate-"+i);
                    label.classList.add("fa"); label.classList.add("fa-star");
                    star_widget.appendChild(label);
                    
                    radio.addEventListener('click',e=>{answers[questionCounter]=e.target.dataset["number"]; }) //lepo upisuje
                }

                let form=document.createElement('form');
                form.setAttribute("action","#");
                star_widget.appendChild(form);
                
                let prazno=document.createElement('header');
                form.appendChild(prazno);
                
                let  tekstic2=document.createElement('div')
                tekstic2.classList.add("textarea");
                form.appendChild(tekstic2);

                let textarena=document.createElement("textarea");
                textarena.setAttribute("cols","30"); 
                textarena.setAttribute("placeholder","Podelite svoje misljenje!");
                tekstic2.appendChild(textarena);

                let Divdugme = document.createElement('div');
                Divdugme.classList.add("btnS");
                form.appendChild(Divdugme);
                let dugme=document.createElement('button');
                dugme.setAttribute("type","submit");
                dugme.innerHTML="Post";
                Divdugme.appendChild(dugme);

                dugme.addEventListener('click',e=>{  //post
                    let flag=true;
                    star_widget.style.display = "none";
                    post.style.display = "block";
                    edit.onclick = ()=>{
                        star_widget.style.display = "block";
                        post.style.display = "none";
                        clearTimeout(timer);
                        div6.innerHTML=PitanjeObj.tekst_pitanja;
                        flag=false;
                    }
                    if(textarena.value!==null || textarena.value!='') {
                        comments[questionCounter]=textarena.value;
                       // answers[questionCounter]="samo radi"; //msm da je upisana ocena u 284 liniju
                    }
                    let timer=setTimeout(NextQuestion,4000);
                    div6.innerHTML="Imate par sekundi da promenite odgovor"             
                })         
            }
        
            else if(TypeQuestion === 2){
                let form_group = document.createElement("div");
                div2.appendChild(form_group);
                form_group.classList.add("form_group");
                let tekst = document.createElement('textarea');
                tekst.classList.add("form_field");
                tekst.setAttribute("placeholder","Name");
                form_group.appendChild(tekst);
                let labela = document.createElement("div");
                labela.setAttribute("for","name");
                labela.classList.add("form_label");
                labela.innerHTML ="Komentar:";
                form_group.appendChild(labela);

                tekst.oninput = function() {
                    tekst.style.height = "";
                    tekst.style.height = tekst.scrollHeight + "px";
                };

                tekst.addEventListener("keypress",e =>{
                    if(e.key==="Enter"){
                        comments[questionCounter]=tekst.value;
                        //answers[questionCounter]="samo radi"
                        console.log(comments[questionCounter]);
                        NextQuestion()
                    }
                })
            }
        }else{
            Login=false;
            let anketaID=Anketa.getAttribute("data-anketaID");
            ankete=ankete.filter(function(anketa){ return anketa.id!=anketaID}) //obrisi anketu koja je radjena 
            div1.remove();  
            let pitanjaID=questions.map((question)=>{return question.id})

            fetch("https://localhost:5001/Odgovor/DodajVratiOdgovore?pitanjaID="+pitanjaID+"&tekst_odgovora="+answers+"&komentarODG="+comments+"&anketaId="+anketaID+"&studentID="+Student.id,{method : "POST"})
            .then(pod=>{
                if(pod.ok){ pod.json().then( card(host,Anketa.getAttribute("data-Entitet"),Anketa.getAttribute("data-anketaID"),pitanjaID))  }   
            })
        }
    }
    return
}

function NextQuestion(){
    div2.innerHTML=""; 
    div2.appendChild(div25);
    window.parent.ShowQuestions();
}

/*napravi bota*/
function assistant(host)
{
        let chatbox = document.createElement('div');
        chatbox.classList.add("chatbox");
        //chatbox.style = "position: relative"; //za 2 istance
            let chatbox__support = document.createElement('div');
            chatbox__support.classList.add("chatbox__support");
            
                let chatbox__header = document.createElement('div');
                chatbox__header.classList.add("chatbox__header");
                
                    let chatbox__image__header = document.createElement('div');
                    chatbox__image__header.classList.add("chatbox__image--header");

                        let elem = document.createElement("img");
                        elem.setAttribute("src","/images/image.png");
                        elem.setAttribute("alt","prva slika"); //alt nbtn
                        chatbox__image__header.appendChild(elem);
                    chatbox__header.appendChild(chatbox__image__header);

                    let chatbox__content__header = document.createElement('div');
                    chatbox__content__header.classList.add("chatbox__content--header");
                        
                        let h4 = document.createElement("h4");
                        h4.classList.add("chatbox__heading--header");
                        const textNode = document.createTextNode("Chat support");
                        h4.appendChild(textNode);
                        let p1 = document.createElement("p");
                        p1. classList.add("chatbox__description--header");
                        p1.innerHTML="Ukoliko zelite da saznate vise o ovoj anketi porazgovarajte samnom";
                        chatbox__content__header.appendChild(h4);
                        chatbox__content__header.appendChild(p1);
                    chatbox__header.appendChild(chatbox__content__header);
                
                chatbox__support.appendChild(chatbox__header);
                
                let chatbox__messages = document.createElement('div');
                chatbox__messages.classList.add("chatbox__messages");
                    let empty = document.createElement('div');
                    empty.classList.add('nesto');
                    chatbox__messages.appendChild(empty);
                chatbox__support.appendChild(chatbox__messages); 
                
                let chatbox__footer = document.createElement('div');
                    chatbox__footer.classList.add("chatbox__footer");
                    let pic1 = document.createElement('img');
                    pic1.setAttribute("src","/images/icons/emojis.svg");
                    pic1.setAttribute("alt","");
                    let pic2 = document.createElement('img');
                    pic2.setAttribute("src","/images/icons/microphone.svg");
                    pic2.setAttribute("alt","");
                    let input = document.createElement('input');
                    input.setAttribute("type","text");
                    input.setAttribute("placeholder","Write a message...");
                    let p = document.createElement('p');
                    p.classList.add("chatbox__send--footer");
                    p.innerHTML="Send";
                    let pic3 = document.createElement('img');
                    pic3.setAttribute("src","/images/icons/attachment.svg");
                    pic3.setAttribute("alt","");
                    chatbox__footer.appendChild(pic1);
                    chatbox__footer.appendChild(pic2);
                    chatbox__footer.appendChild(input);
                    chatbox__footer.appendChild(p);
                    chatbox__footer.appendChild(pic3);
                chatbox__support.appendChild(chatbox__footer);
            
            let chatbox__button = document.createElement('div');
            chatbox__button.classList.add("chatbox__button");
                let butt = document.createElement('button');
            chatbox__button.appendChild(butt);
        
        chatbox.appendChild(chatbox__support);
        chatbox.appendChild(chatbox__button);
    
    host.appendChild(chatbox);

box(host); //poziva da se iscrtava chatbox
bot(host); //pokrece bota
}
