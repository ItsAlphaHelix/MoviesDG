const chatButton = document.querySelector('.chatbox__button');
const chatContent = document.querySelector('.chatbox__support');
const sectionElement = document.querySelector('.banner-area');
const sectionTwoElement = document.querySelector('.ucm-area');

const icons = {
    isClicked: '<img src="./images/chatbox-icon.svg" />',
    isNotClicked: '<img src="./images/chatbox-icon.svg" />',
}
const chatbox = new InteractiveChatbox(chatButton, chatContent, icons);
chatbox.display();
chatbox.toggleIcon(false, chatButton);


sectionElement.addEventListener('click', function () {
    chatContent.classList.remove('chatbox--active');
});

sectionTwoElement.addEventListener('click', function () {
    chatContent.classList.remove('chatbox--active');
});