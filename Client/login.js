import { introduce } from "./app.js";
export var Student; //id, ime, prezime, trebalo bi i popunjuavanje id 

export function Login(host)
{
    host.innerHTML=''
    fetch(`https://localhost:5001/Fakultet/PreuzmiFakultet/${idFaksa}`,{method:"GET"})
    .then(s=>{
        if(s.ok){
            s.json()
            .then(Faks=> {
                Faks===null?alert("greska"):faks=Faks
                dalje()    
                console.log(faks);
            })
        }
    })
    function dalje(){
        let div1=document.createElement("div");
        div1.classList.add("login-div");
        host.appendChild(div1);

        let div2=document.createElement("div");
        div2.classList.add("logo");
        idFaksa==1?div2.style.background="url(/logo1.jpg)":div2.style.background="url(/logo2.jpg)";
        div1.appendChild(div2);
        
        let div3=document.createElement("div");
        div3.classList.add("title");
        div1.appendChild(div3);
        div3.innerHTML=faks.naziv;

        let div4= document.createElement("div");
        div4.classList.add("sub-title");
        div1.appendChild(div4);
        div4.innerHTML=faks.info;

        let div5=document.createElement("div");
        div5.classList.add("fields");
        div1.appendChild(div5);

        let div6=document.createElement("div");
        div6.classList.add("username");
        div5.appendChild(div6);
        
        let svg1 = document.createElement("img");
        svg1.setAttribute("src","/img/user-icon.png");
        svg1.setAttribute("alt","user slika"); 
        svg1.classList.add("svg-icon");
        svg1.setAttribute("viewBox","0 0 20 20");
        div6.appendChild(svg1);

        let email=document.createElement("input");
        email.setAttribute("type","username");
        email.classList.add("user-input");
        email.placeholder="mail";
        div6.appendChild(email);

        let div62=document.createElement("div");
        div62.classList.add("password");
        div5.appendChild(div62);

        let svg2 = document.createElement("img");
        svg2.setAttribute("src","/img/lock-icon.png");
        svg2.setAttribute("alt","lock slika"); 
        svg2.classList.add("svg-icon");
        svg2.setAttribute("viewBox","0 0 20 20")
        div62.appendChild(svg2); 

        let pass=document.createElement("input");
        pass.setAttribute("type","password");
        pass.classList.add("pass-input");
        pass.placeholder="sifra";
        div62.appendChild(pass);

        let logDugme=document.createElement("button");
        logDugme.classList.add("signin-button")
        logDugme.innerHTML="Logovanje";
        div1.appendChild(logDugme);

        let div9 = document.createElement("div");
        div9.classList.add("link");
        div1.appendChild(div9);

        let logAdmin=document.createElement("a");
        div9.appendChild(logAdmin);
        logAdmin.innerHTML="Ulogujte se kao admin";
        logAdmin.setAttribute("href","#");
        logAdmin.addEventListener('click',e=>{
            if(email.value==="")
                alert("Molimo Vas ukucajte lepo mail");
            else if(pass.value==="")
                alert("Molimo Vas ukucajte lepo sifru");
            else{
                fetch(`https://localhost:5001/Administrator/VratitiAdministratora?username=${email.value}&sifra=${pass.value}`,{method:"GET"})
                .then(s=>{
                    if(s.ok){
                        s.json().then(admin=>{
                            if(admin===null) alert("ukucajte lepo podatke")
                            else{
                                AdminLogin(host)
                            }
                        })  
                    }else alert("nepostojeci admin");
                })
            }
        })
        
        logDugme.addEventListener("click",(nesto)=>{
            nesto.preventDefault();
            nesto.stopPropagation();
            nesto.cancelBubble=true;
                if(email.value==="")
                    alert("Molimo Vas ukucajte lepo mail");
                if(pass.value==="")
                    alert("Molimo Vas ukucajte lepo sifru");
                else{

                    fetch("https://localhost:5001/Student/PreuzmiStudenta/"+email.value+"/"+pass.value,{  method: "GET" })
                    .then(s=>{
                        if(s.ok){
                            s.json().then(student=>{
                                if(student===null)
                                    alert("ukucaj lepo podatke!");
                                else 
                                    Student=student;
                                while(host.hasChildNodes())     
                                host.removeChild(host.lastChild); //ovo sve brise sve ispod div0 tj istancu logina
                                introduce(host,idFaksa); 
                            })

                        }else alert("nepostojeci student");
                    })
                    
                }
            } 
        )
    }
}
                           //pazi na ovo
let faks,fakulteti=[],ImeFaksa,idFaksa=1,div8,idAnketa,divAnketa,pAnketa,ankete,pitanja,div47,divPitanja,pPitanja,div48,divTekst,labelTekst,inputTekst,divOdgovor,labelOdgovor,inputOdgovor,div501,div502,div503,div11,div22,divAnka,labelAnka,inputAnka;
function AdminLogin(host){
    fakulteti=[]
    host.innerHTML='' 
    fetch(`https://localhost:5001/Fakultet/PreuzmiFakultete`,{method:"GET"})
    .then(s=>{
        if(s.ok){
            s.json()
            .then(faksovi=>{
                if(faksovi===null) alert("Greska");
                else {
                    faksovi.forEach(faks => {
                        fakulteti.push(faks);
                    });
                }
                Dalje()
            })
        }
    })
    
    function Dalje(){
        
        div8=document.createElement("form");
        host.appendChild(div8);
        div8.classList.add("survey-form");

        let div37=document.createElement("div");
        div8.appendChild(div37);
        div37.classList.add("form-group");
        
        let div38=document.createElement("p");
        div37.appendChild(div38);
        div38.innerText="Izaberite fakultet za koji be ste zeleli da se radi anketa i da promenite podatke"
        
        fakulteti.forEach(faks=>{
        
            let label39=document.createElement("label");
            div37.appendChild(label39);
            label39.innerHTML=faks.naziv;
            
            let input40=document.createElement("input");
            label39.prepend(input40);
            input40.setAttribute("name","user-recommend")
            input40.setAttribute("value",faks.naziv)
            input40.setAttribute("faksID",faks.id)
            input40.setAttribute("type","radio")
            input40.classList.add("input-radio");

            input40.addEventListener("click",e=>{
                ImeFaksa=input40.value;
                idFaksa=input40.getAttribute("faksID");
                DobaviAnkete();
            });
        })

        let div9=document.createElement('div');
        div8.appendChild(div9);
        div9.classList.add("form-group");

        let div10=document.createElement("label");
        div9.appendChild(div10);
        div10.setAttribute("for","name");
        div10.innerText="Naziv fakulteta"
        
         div11=document.createElement("input");
        div9.appendChild(div11);
        div11.setAttribute("text","text");
        div11.setAttribute("name","name");
        div11.setAttribute("placeholder","Ako zelite da promenite, ukucajte ovde");
        div11.classList.add("form-control")

        let div20=document.createElement('div');
        div8.appendChild(div20);
        div20.classList.add("form-group");

        let div21=document.createElement("label");
        div20.appendChild(div21);
        div21.setAttribute("for","info");
        div21.innerText="Informacije fakulteta"
        
         div22=document.createElement("input");
        div20.appendChild(div22);
        div22.setAttribute("text","text");
        div22.setAttribute("name","info");
        div22.setAttribute("placeholder","Ako zelite da promenite, ukucajte ovde");
        div22.classList.add("form-control");
        
        divAnketa=document.createElement("div");
        div8.appendChild(divAnketa);
        divAnketa.classList.add("form-group");  
        pAnketa=document.createElement("p");
        divAnketa.appendChild(pAnketa);
        pAnketa.innerText="Dostupne ankete za izabrani fakultet su"

        divAnka=document.createElement('div');
        div8.appendChild(divAnka);
        divAnka.classList.add("form-group");

        labelAnka=document.createElement("label");
        divAnka.appendChild(labelAnka);
        labelAnka.setAttribute("for","name");
        labelAnka.innerText="Pazljivo promenite anketu:"
        
        inputAnka=document.createElement("input");
        divAnka.appendChild(inputAnka);
        inputAnka.setAttribute("text","text");
        inputAnka.setAttribute("name","name");
       inputAnka.setAttribute("placeholder","Info.Link.Mail.Naziv.Telefon");
       inputAnka.classList.add("form-control")

        div47=document.createElement("select");
        divAnketa.appendChild(div47);
        div47.setAttribute("name","role");
        div47.classList.add("form-control");

        let div45=document.createElement('div');
        div8.appendChild(div45);
        div45.classList.add("form-group")

        let div46=document.createElement('p');
        div45.appendChild(div46);
        div46.innerText="Sta zelite da uradite za datu anketu?"

        
        let div491=document.createElement('label');  ///ponvljeno
        div491.innerHTML="Dodati pitanje";
        div45.appendChild(div491);
         div501=document.createElement('input');
        div491.prepend(div501);
        div501.setAttribute("name","user-recommended");
        div501.setAttribute("value","Dodati");
        div501.setAttribute("type","radio");
        div501.classList.add("input-radio");
        
        let div492=document.createElement('label');   ///ponvljeno
        div492.innerHTML="Promeniti pitanje";
        div45.appendChild(div492);
         div502=document.createElement('input');
        div492.prepend(div502);
        div502.setAttribute("name","user-recommended");
        div502.setAttribute("value","Promeniti");
        div502.setAttribute("type","radio");
        div502.classList.add("input-radio");

        let div493=document.createElement('label');    ///ponvljeno
        div493.innerHTML="Izbrisati pitanje";
        div45.appendChild(div493);
         div503=document.createElement('input');
        div493.prepend(div503);
        div503.setAttribute("name","user-recommended");
        div503.setAttribute("value","Izbrisati");
        div503.setAttribute("type","radio");
        div503.classList.add("input-radio");
        div501.addEventListener("click",e=>{})
        div502.addEventListener("click",e=>{})
        div503.addEventListener("click",e=>{})


        divPitanja=document.createElement("div"); 
        div8.appendChild(divPitanja);
        divPitanja.classList.add("form-group");  
        pPitanja=document.createElement("p");
        divPitanja.appendChild(pPitanja);
        pPitanja.innerText="Dostupna pitanja za izabranu anketu su"
        div48=document.createElement("select");
        divPitanja.appendChild(div48);
        div48.setAttribute("name","role");
        div48.classList.add("form-control");
        
        divTekst=document.createElement('div');
        div8.appendChild(divTekst);
        divTekst.classList.add("form-group");

        labelTekst=document.createElement("label");
        divTekst.appendChild(labelTekst);
        labelTekst.setAttribute("for","name");
        labelTekst.innerText="Pitanje glasi:"
        
        inputTekst=document.createElement("input");
        divTekst.appendChild(inputTekst);
        inputTekst.setAttribute("text","text");
        inputTekst.setAttribute("name","name");
        inputTekst.setAttribute("placeholder","Nedostupno");
        inputTekst.classList.add("form-control")

        divOdgovor=document.createElement('div');
        div8.appendChild(divOdgovor);
        divOdgovor.classList.add("form-group")
        labelOdgovor=document.createElement("label");
        divOdgovor.appendChild(labelOdgovor);
        labelOdgovor.setAttribute("for","name");
        labelOdgovor.innerText="Odgovor glasi:"
        inputOdgovor=document.createElement("input");
        divOdgovor.appendChild(inputOdgovor);
        inputOdgovor.setAttribute("text","text");
        inputOdgovor.setAttribute("name","name");
        inputOdgovor.setAttribute("placeholder","Oceni, Napisi, Odgovor1.Odgovor2");
        inputOdgovor.classList.add("form-control")

        let div206=document.createElement("form-group");
        div8.appendChild(div206);
        let div207=document.createElement('button');
        div207.innerText="Potvrdi"
        div206.appendChild(div207);
        div207.setAttribute("type","submit");
        div207.classList.add("submit-button");
        div207.addEventListener("click",e=>Submit())

        function DobaviAnkete(){
            ankete=[]; //id, entitet
            fetch(`https://localhost:5001/Fakultet/PreuzmiAnketeZaFaks?naziv=${ImeFaksa}`,{method:"GET"})
            .then(s=>{
                if(s.ok){
                    s.json()
                        .then(Ankete=>{
                            if(Ankete===null)alert("problem sa nabavkom anketa");
                            else{
                                Ankete.forEach(anketa=>{
                                    ankete.push(anketa);
                            })}
                            PrikaziAnkete();
                        })
                }
            })
        }
    }
    function PrikaziAnkete(){
        
        if(divAnketa.lastChild.hasChildNodes) divAnketa.lastChild.innerHTML=''
        let div90=document.createElement("option");
        div90.innerText="Izaberite"
        div90.setAttribute("disabled","true");
        div90.setAttribute("selected","true");
        div47.appendChild(div90)
        ankete.forEach(anketa=>{
            let div49=document.createElement("option");
            if(anketa.entitet===0) div49.innerText="Zaposleni";
            else if(anketa.entitet===1) div49.innerText="Lokacija";
            else if(anketa.entitet===2) div49.innerText="Organizacija";
            div49.setAttribute("value",anketa.entitet);
            div49.setAttribute("idAnkete",anketa.id);
            div47.appendChild(div49);
            
        })
        div47.addEventListener("change",e=>{
            DobaviPitanja(div47.value)
            idAnketa=ankete.find(anketa=>anketa.entitet==div47.value).id; //radi
        })
    }
    function DobaviPitanja(entitet){
        pitanja=[];
        fetch(`https://localhost:5001/Pitanje/PreuzmiPitanja/${entitet}`,{method:"GET"})
        .then(s=>{
            if(s.ok){
                s.json()
                .then(Pitanja=>{
                    if(Pitanja===null)alert("greska bi nabavci pitanja")
                    else{
                        Pitanja.forEach(Pitanje => {
                            pitanja.push(Pitanje);
                        })
                    }
                    PrikaziPitanja()
                })
            }
        })
    }
    function PrikaziPitanja(){
        if(divPitanja.lastChild.hasChildNodes) 
            divPitanja.lastChild.innerHTML=''
        pitanja.forEach(pitanje=>{
            let div49=document.createElement("option");
            div49.innerText=pitanje.tekst_pitanja;
            div49.setAttribute("value",pitanje.id);
            div48.appendChild(div49);
        })
    }

    function Submit(){  //ne treba mi onda values od radio buttona
        if(div11.value!==""||div22.value!==""){

            fetch(`https://localhost:5001/Fakultet/PromenitiFakultet?faksID=${idFaksa}&naziv=${div11.value}&info=${div22.value}`,{method:"PUT"})
                .then(s=>{
                    if(s.ok){ s.json().then(nesto=>{
                        if(nesto===null)alert("nije dobra promena fakulteta")
                    })
                }})
        }
        //menjam atribute ankete
        if(idAnketa!==""){ 
            let atributi=inputAnka.value.split('.')
            let a0=atributi[0];
            let a1=atributi[1];
            let a2=atributi[2];
            let a3=atributi[3];
            let a4=atributi[4];
           
            fetch(`https://localhost:5001/Anketa/PromeniAnketu/${idAnketa}/${a0}/${a1}/${a2}/${a3}/${a4}`,{method:"PUT"})
                .then(s=>{
                    if(s.ok){ s.json().then(nesto=>{
                        if(nesto===null)alert("nije dobar upis pitanja")
                        Ostani(host)
                    })
                }})
        }

        if(div501.checked||div502.checked||div503.checked){
            let textOdgovora=inputOdgovor.value;
            let tip;
            if(textOdgovora.includes('.')) {
                tip=0;
                textOdgovora.split('.');
            }
            else if(textOdgovora==="Oceni") {tip=1;textOdgovora=null;}
            else if(textOdgovora==="Napisi") {tip=2;textOdgovora=null;}
            else if(!div503.checked){    alert("lepo napisite odgovor");return; }
            if(inputTekst.value===""&& !div503.checked) alert("napisite pitanje!")

            if(div501.checked){  //dodaj pitanje
                fetch(`https://localhost:5001/Pitanje/DodatiPitanje/${inputTekst.value}/${idAnketa}/${tip}/${textOdgovora}`,{method:"POST"})
                .then(s=>{
                    if(s.ok){ s.json().then(nesto=>{
                        if(nesto===null)alert("nije dobar upis pitanja")
                        Ostani(host)
                    })
                }})
            }
            if(div502.checked){ //promeni pitanje
                let pitanjeID=div48.options[div48.selectedIndex].value;
                fetch(`https://localhost:5001/Pitanje/PromenitiPitanje/${pitanjeID}/${inputTekst.value}/${textOdgovora}`,{method:"PUT"})
                .then(s=>{
                    if(s.ok){ s.json().then(nesto=>{
                        if(nesto===null)alert("nije dobrA promena pitanja")
                        Ostani(host)
                    })
                }})
            }
            if(div503.checked){  //izbrisi pitanje
                let pitanjeID=div48.options[div48.selectedIndex].value;
                fetch(`https://localhost:5001/Pitanje/IzbrisiPitanje/${pitanjeID}`,{method:"DELETE"})
                .then(s=>{
                    if(s.ok){ s.json().then(nesto=>{
                        if(nesto===null)alert("nije dobro obrisano pitanje")
                        Ostani(host)
                    })
                }})
            }
        }
        Ostani(host)
    }
}
function Ostani(host){
    let ostani=confirm("Da li zelite nazad?")
    ostani==true?Login(host):AdminLogin(host)
}

let div0=document.createElement("div");
document.body.appendChild(div0);
Login(div0);
//let div01=document.createElement("div");
//document.body.appendChild(div01); 
//Login(div01);
 //display overflow bi mogo bi mogo on da bude
 




