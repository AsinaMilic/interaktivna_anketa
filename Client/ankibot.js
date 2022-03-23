import { introduce } from "./app.js";
import { chat } from "./conversation.js";

export const bot = function (host) {

    const container = host.querySelector('.chatbox__support'); 
    let dodaj = host.querySelector('.nesto');  

    const sleep = function (ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    };

    const disableAllChoices = function () {
        const choices = host.querySelectorAll('.choice');
        choices.forEach(function (choice) {
            choice.disabled = 'disabled';
        });
        return;
    };
    
    const printChoice = function (choice) {
        const choiceElem = document.createElement('div');
        choiceElem.classList.add('chat-ask');
        choiceElem.innerHTML = choice.innerHTML;
        insertNewChatItem(choiceElem);
    };
    
    const handleChoice = async function (e) {

        if (!e.target.classList.contains('choice') || 'A' === e.target.tagName) { //da li sam izabrao neki choice ili sam tamo nesto kliknuo, <a t
            var button = e.target.closest('#chatbox__support .choice');  //button dobija vrednost kad se pritisne restart , ENG,WEB,OS
            if (button !== null) {
                button.click();
                x=button;
            }
            return;
        }

        e.preventDefault();
        const choice = e.target;//radi!
        if(choice.innerText==="SLEDECA ANKETA"){
            host.innerHTML=""
            introduce(host) 
        }
            
        disableAllChoices();
        printChoice(choice);
        await sleep(1500);

        if (choice.dataset.next) {
            printResponse(chat[choice.dataset.next]);
        }
        
    };

    const startConversation = function () { 
        printResponse(chat[1]);
    }

    const printResponse = async function (step) {
        const response = document.createElement('div');
        response.classList.add('messages__item');
        response.classList.add('messages__item--visitor');
        
        response.innerHTML = step.text;
        insertNewChatItem(response);

        await sleep(1500);

        if (step.options) {
            const choices = document.createElement('div');
            choices.classList.add('choices');
            step.options.forEach(function (option) {
                const button = document.createElement(option.url ? 'a' : 'button');
                button.classList.add('choice');
                
                button.innerHTML = option.text;
                if (option.url) {
                    button.href = option.url;
                } else {
                    button.dataset.next = option.next;
                }
                choices.appendChild(button);
            });
            insertNewChatItem(choices);
        } else if (step.next) {
            printResponse(chat[step.next]);
        }
    };
    

    const insertNewChatItem = function (elem) {
        dodaj.appendChild(elem);
        elem.classList.add('activated');
    };

    const init = function () {   //prvo ide init
        container.addEventListener('click', handleChoice);
        startConversation();  
    };

    init();
}
