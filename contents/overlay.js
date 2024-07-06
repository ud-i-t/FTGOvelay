// get DOM elements
const centerTopText = document.getElementById("centerTopText");

const player1Elements = {
    name: document.getElementById("playerName1"),
    image: document.getElementById("player1Images"),
    index: -1
}
const score1 = document.getElementById("score1");
const teamScore1 = document.getElementById("teamScore1");

const player2Elements = {
    name: document.getElementById("playerName2"),
    image: document.getElementById("player2Images"),
    index: -1
}
const score2 = document.getElementById("score2");
const teamScore2 = document.getElementById("teamScore2");

// functions
function createPlayerImage(fileName) {
    var img = document.createElement("img");
    img.setAttribute("src", fileName);
    img.style.display = "none";
    return img;
}

function switchPlayer(elements, index) {
    if (elements.index == index) {
        return;
    }

    if (elements.index >= 0) {
        elements.image.children[elements.index].style.display = "none";
    }
    elements.image.children[index].style.display = "block";
    elements.name.innerHTML = players[index].name;
    elements.index = index;
}

function updateText() {
    let obj;
    readScore = async () => {
        const response = await fetch('score.json');
        const data = await response.text();
        obj = JSON.parse(data);
    }
    readScore().then(() => {
        const newP1Index = obj["player1"];
        switchPlayer(player1Elements, newP1Index);
        const newP2Index = obj["player2"];
        switchPlayer(player2Elements, newP2Index);

        score1.innerHTML = obj["score1"];
        score2.innerHTML = obj["score2"];

        teamScore1.innerHTML = obj["teamScore1"];
        teamScore2.innerHTML = obj["teamScore2"];

        centerTopText.innerHTML = obj["centerTopText"];
    });
}

function main() {
    updateText();
    setInterval(updateText, 1000);
}

// ページ表示時にプレイヤーデータをキャッシュしておく
let players;
readPlayers = async () => {
    const response = await fetch('players.json');
    const data = await response.text();
    const obj = JSON.parse(data);
    players = obj["players"];
    console.log(players);

    {
        player = player1Elements.image;
        players.forEach(element => {
            let img = createPlayerImage("players/" + element.player + "_L.png");
            player.appendChild(img);
        });
    }
    {
        player = player2Elements.image;
        players.forEach(element => {
            let img = createPlayerImage("players/" + element.player + "_R.png");
            player.appendChild(img);
        });
    }
}
readPlayers().then(main);