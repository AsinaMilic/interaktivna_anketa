import { introduce } from "./app.js";
export function card(host,entitet,ankataid,pitanjaID)
{
    let EntitetPod;
    fetch("https://localhost:5001/Anketa/PreuzmiAnketu?Anketaid="+ankataid,{method:"GET"})
    .then(s=>{if(s.ok){s.json()
        .then(anketaObj=>{
            EntitetPod=anketaObj
            console.log(EntitetPod);
            Show()
        })
    }})

    function Show(){
        let OdgovoriStudenti=[];
        fetch(`https://localhost:5001/Odgovor/VratiStudenteOdgovora?pitanjaID=${pitanjaID}`,{method : "GET"})
        .then(s=>{
            if(s.ok){
                s.json()
                    .then(OdgovoriStudenataArr=>{
                        OdgovoriStudenataArr.forEach(OdgovorStudentaArr=>{
                            OdgovorStudentaArr.forEach(OdgovorStudent=>{
                                OdgovoriStudenti.push(OdgovorStudent)
                                console.log(OdgovorStudent);
                            })
                        })
                        console.log(OdgovoriStudenti)
                        Ispisi();
                    })
            }
        })
        function Ispisi(){

            let div11=document.createElement("div")
            host.prepend(div11)
            div11.classList.add("card")
            div11.setAttribute("data-state","#about")

            let div12=document.createElement("div")
            div11.appendChild(div12)
            div12.classList.add("card-header")

            let div165=document.createElement("h2") //dodao ja         
            let ocena=0;
            let i=0;
            OdgovoriStudenti.forEach(OdgovorStudent=>{
                if(OdgovorStudent.tekst_odgovora.length==1){
                    ocena+=parseInt(OdgovorStudent.tekst_odgovora)
                    i++;
                } 
            })          
            ocena/=i;
            if(i!==0){
                if(ocena>=1 && ocena<1.5) div165.innerText="⭐"
                else if(ocena>=1.5 && ocena<2.5) div165.innerText="⭐⭐"
                else if(ocena>=2.5 && ocena<3.5) div165.innerText="⭐⭐⭐"
                else if(ocena>=3.5 && ocena<4.5) div165.innerText="⭐⭐⭐⭐"
                else if(ocena>=4.5 && ocena<=5) div165.innerText="⭐⭐⭐⭐⭐"
            }
            div165.style.padding = "0 85px";
            
            div11.appendChild(div165)
            let div13=document.createElement("div")
            div12.appendChild(div13)
            div13.classList.add("card-cover")
        
            let div14=document.createElement("img")
            div14.setAttribute("src",`/images/${entitet}.gif`);
            div12.appendChild(div14)
            
            div14.setAttribute("alt","Avatar")
            div14.classList.add("card-avatar")

            let div15=document.createElement("h1")
            div12.appendChild(div15)
            div15.classList.add("card-fullname")
        
            EntitetPod.info.replace('.','\n');
            div15.innerText=EntitetPod.naziv 

            let div16=document.createElement("h2")
            div12.appendChild(div16)
            div16.classList.add("card-jobtitle")
            switch (entitet) {
                case 0: div16.innerText="ZAPOSLENI"; break; 
                case 1: div16.innerText="LOKACIJA"; break; 
                case 2: div16.innerText="ORGANIZACIJA"; break;  
            }
            
            let div18=document.createElement("div")
            div11.appendChild(div18)
            div18.classList.add('card-main')

            let div19=document.createElement("div")
            div18.appendChild(div19)
            div19.classList.add("card-section")
            div19.classList.add("is-active")
            div19.setAttribute("id","about")

            let div20=document.createElement("div")
            div19.appendChild(div20)
            div20.classList.add("card-content")

            let div21=document.createElement("div")
            div20.appendChild(div21)
            div21.classList.add("card-subtitle")
            div21.innerText = "INFORMACIJE"

            let div22=document.createElement("div")
            div20.appendChild(div22)
            div22.classList.add("card-desc")
            div22.innerText=EntitetPod.info

            let div31=document.createElement("div")
            div18.appendChild(div31)
            div31.classList.add("card-section")
            div31.setAttribute("id","experience")

            let div32=document.createElement("div")
            div31.appendChild(div32)
            div32.classList.add("card-content")

            let div33=document.createElement("div")
            div32.appendChild(div33)
            div33.classList.add("card-subtitle")
            div33.innerText="DODATNI KOMENTARI" 

            let div34=document.createElement("div")
            div32.appendChild(div34)
            div34.classList.add("card-timeline")

            let div35=document.createElement("div")
            div34.appendChild(div35)
            div35.classList.add("card-item")
            div35.setAttribute("data-year","2022") 
                
            i=-1;
            OdgovoriStudenti.forEach(OdgovorStudent=>{          
                i++;
                if(OdgovorStudent.komentar!==null &&OdgovorStudent.komentar!==""){
                    let div36=document.createElement("div")
                    div35.appendChild(div36)
                    div36.classList.add("card-item-title")
                    div36.innerText=OdgovoriStudenti[i].ime+" "+OdgovoriStudenti[i].prezime;               

                    let div37=document.createElement("div")
                    div35.appendChild(div37)
                    div37.classList.add("card-content")
                    div37.innerText=OdgovoriStudenti[i].komentar
                
                }
            })

            let div54=document.createElement("div")
            div18.appendChild(div54)
            div54.classList.add("card-section")
            div54.setAttribute("id","contact")

            let div55=document.createElement("div")
            div54.appendChild(div55)
            div55.classList.add("card-content")

            let div56=document.createElement("div")
            div55.appendChild(div56)
            div56.classList.add("card-subtitle")
            div56.innerHTML="Contact"

            let div57=document.createElement("div")
            div55.appendChild(div57)
            div57.classList.add("card-contact-wrapper")

            let div58=document.createElement("div")
            div57.appendChild(div58)
            div58.classList.add("card-contact")
            div58.innerText=EntitetPod.link  

            let div59=document.createElement("img")
            div58.prepend(div59);
            div59.setAttribute("src","/images/location.png")
            div59.setAttribute("alt","")


            let div62=document.createElement("div")
            div55.appendChild(div62)
            div62.classList.add("card-contact")
            div62.innerText=EntitetPod.telefon 

            let div63=document.createElement("img")
            div62.prepend(div63);
            div63.setAttribute("src","/images/phone.png")
            div63.setAttribute("alt","")
            
            let div66=document.createElement("div");
            div55.appendChild(div66);
            div66.classList.add("card-contact")
            div66.innerText=EntitetPod.mail  
            let div67=document.createElement("img");
            div66.prepend(div67)
            div67.setAttribute("src","/images/mail.png")
            div67.setAttribute("alt","")         

            let div69=document.createElement("div")
            div55.appendChild(div69)
            let div70=document.createElement("button")
            div70.classList.add("contact-me");
            div70.innerText="URADITE JOS ANKETA"
            div69.appendChild(div70)

            let div74=document.createElement("div")
            div18.appendChild(div74)
            div74.classList.add("card-buttons")

            let div75=document.createElement("button")
            div74.appendChild(div75)
            div75.classList.add("is-active")
            div75.setAttribute("data-section","#about")
            div75.innerText="INFORMACIJE"

            let div76=document.createElement("button")
            div74.appendChild(div76)
            div76.setAttribute("data-section","#experience")
            div76.innerText="KOMENTARI"

            let div77=document.createElement("button")
            div74.appendChild(div77)
            div77.setAttribute("data-section","#contact")
            div77.innerText="DODATNO"
        
            const buttons = document.querySelectorAll(".card-buttons button");
            const sections = document.querySelectorAll(".card-section");
            const card = document.querySelector(".card");

            const handleButtonClick = e => {
                const targetSection = e.target.getAttribute("data-section");
                const section = document.querySelector(targetSection);
                targetSection !== "#about" ? card.classList.add("is-active") : card.classList.remove("is-active");
                
                card.setAttribute("data-state", targetSection);
                sections.forEach(s => s.classList.remove("is-active"));
                buttons.forEach(b => b.classList.remove("is-active"));
                e.target.classList.add("is-active");
                section.classList.add("is-active");
            }
            buttons.forEach(btn => {
                btn.addEventListener("click", handleButtonClick);
            })
            div70.addEventListener('click',e=>{
                host.removeChild(host.firstChild);host.removeChild(host.firstChild)
                introduce(host)
            })
        }
    }
}