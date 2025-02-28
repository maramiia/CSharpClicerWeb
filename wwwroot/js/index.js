const threshold = 10;
let seconds = 0;
let clicks = 0;
const currentScoreElement = document.getElementById("current_score");
const recordScoreElement = document.getElementById("record_score");
const profitPerClickElement = document.getElementById("profit_per_click");
const profitPerSecondElement = document.getElementById("profit_per_second");
let currentScore = Number(currentScoreElement.innerText);
let recordScore = Number(recordScoreElement.innerText);
let profitPerSecond = Number(profitPerSecondElement.innerText);
let profitPerClick = Number(profitPerClickElement.innerText);
let currentPower = document.getElementById("current_power");
let currentProtection = document.getElementById("current_protection")
let totalStats = Number(currentPower.innerText) + Number(currentProtection.innerText);




$(document).ready(function () {
    const clickitem = document.getElementById("clickitem");

    clickitem.onclick = click;
    setInterval(addSecond, 1000)

    const boostButtons = document.getElementsByClassName("boost-button");
    const armorsButtons = document.getElementsByClassName("armor-button");
    const weaponsButtons = document.getElementsByClassName("weapon-button");
    const bossesButtons = document.getElementsByClassName("boss-button");


    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        boostButton.onclick = () => boostButtonClick(boostButton);
    }
    for (let i = 0; i < armorsButtons.length; i++) {
        const armorsButton = armorsButtons[i];

        armorsButton.onclick = () => armorsButtonClick(armorsButton);
    }
    for (let i = 0; i < weaponsButtons.length; i++) {
        const weaponsButton = weaponsButtons[i];

        weaponsButton.onclick = () => weaponsButtonClick(weaponsButton);
    }
    for (let i = 0; i < bossesButtons.length; i++) {
        const bossesButton = bossesButtons[i];

        bossesButton.onclick = () => bossesButtonClick(bossesButton);
    }

    toggleBoostsAvailability();
    toggleArmorsAvailability();
    toggleWeaponsAvailability();
    toggleBossesAvailability();

})

function boostButtonClick(boostButton) {
    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }
    buyBoost(boostButton);
}
function armorsButtonClick(boostButton) {
    console.log("Клик по кнопке брони:", boostButton); // Проверка, срабатывает ли клик

    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }
    buyArmor(boostButton);
}
function weaponsButtonClick(weaponsButton) {
    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }
    buyWeapon(weaponsButton);
}
function bossesButtonClick(bossesButton) {
    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }
    buyBoss(bossesButton);
}
function buyBoost(boostButton) {
    const boostIdElement = boostButton.getElementsByClassName("boost-id")[0];
    const boostId = boostIdElement.innerText;

    $.ajax({
        url: '/boost/buy',
        method: 'post',
        dataType: 'json',
        data: { boostId: boostId },
        success: (response) => onBuyBoostSuccess(response, boostButton),
    });
}
function buyArmor(armorButton) {
    const armorIdElement = armorButton.getElementsByClassName("armor-id")[0];
    const armorId = armorIdElement.innerText; // Исправлено на armorId

    $.ajax({
        url: '/armor/buy',
        method: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify({ armorId: armorId }), // Исправлено на armorId
        success: (response) => onBuyArmorSuccess(response, armorButton),
        error: (xhr, status, error) => {
            console.error("Ошибка запроса:", error, xhr.responseText);
        },
    });
}
function buyWeapon(weaponButton) {
    const weaponIdElement = weaponButton.getElementsByClassName("weapon-id")[0];
    const weaponId = weaponIdElement.innerText; // Исправлено на weaponId

    $.ajax({
        url: '/weapon/buy',
        method: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify({ weaponId: weaponId }),
        success: (response) => onBuyWeaponSuccess(response, weaponButton),
        error: (xhr, status, error) => {
            console.error("Ошибка запроса:", error, xhr.responseText);
        },
    });
}

/*function onBuyBoostSuccess(response, boostButton) {
    const score = response["score"];

    const boostPriceElement = boostButton.getElementsByClassName("boost-price")[0];
    const boostQuantityElement = boostButton.getElementsByClassName("boost-quantity")[0];

    const boostPrice = Number(response["price"]);
    const boostQuantity = Number(response["quantity"]);

    boostPriceElement.innerText = boostPrice;
    boostQuantityElement.innerText = boostQuantity;

    updateScoreFromApi(score);
}*/

function onBuyBoostSuccess(response, boostButton) {
    const score = response["score"];

    const boostPriceElement = boostButton.getElementsByClassName("boost-price")[0];
    const boostQuantityElement = boostButton.getElementsByClassName("boost-quantity")[0];

    const boostPrice = Number(response["price"]);
    const boostQuantity = Number(response["quantity"]);

    boostPriceElement.innerText = boostPrice;
    boostQuantityElement.innerText = boostQuantity;

    // Обновляем текущий счет и характеристики
    updateScoreFromApi(score);
}

function onBuyArmorSuccess(response, armorButton) {
    const score = response["score"];

    const armorPriceElement = armorButton.getElementsByClassName("armor-price")[0];
    const armorQuantityElement = armorButton.getElementsByClassName("armor-quantity")[0];

    const armorPrice = Number(response["price"]);
    const armorQuantity = Number(response["quantity"]);

    armorPriceElement.innerText = armorPrice;
    armorQuantityElement.innerText = armorQuantity;

    updateScoreFromApi(score);
}

function onBuyWeaponSuccess(response, weaponButton) {
    const score = response["score"];

    const weaponPriceElement = weaponButton.getElementsByClassName("weapon-price")[0];
   

    const weaponPrice = Number(response["price"]);
    
    weaponPriceElement.innerText = weaponPrice;
    

    updateScoreFromApi(score);
}


function addSecond() {
    seconds++;

    if (seconds >= threshold) {
        addPointsToScore();
    }

    if (seconds > 0) {
        addPointsFromSecond();
    }
}

function click() {
    clicks++;

    if (clicks >= threshold) {
        addPointsToScore();
    }

    if (clicks > 0) {
        addPointsFromClick();
    }
}

function updateScoreFromApi(scoreData) {
    currentScore = Number(scoreData["currentScore"]);
    recordScore = Number(scoreData["recordScore"]);
    profitPerClick = Number(scoreData["profitPerClick"]);
    profitPerSecond = Number(scoreData["profitPerSecond"]);

    currentPower.innerText = scoreData["power"];
    currentProtection.innerText = scoreData["protection"];

    updateUiScore();
}



function updateUiScore(scoreData = null) {
    if (scoreData) {
        currentScore = Number(scoreData["currentScore"]);
        recordScore = Number(scoreData["recordScore"]);
        profitPerClick = Number(scoreData["profitPerClick"]);
        profitPerSecond = Number(scoreData["profitPerSecond"]);

        currentPower.innerText = scoreData["power"];
        currentProtection.innerText = scoreData["protection"];
    }

    currentScoreElement.innerText = currentScore;
    recordScoreElement.innerText = recordScore;
    profitPerClickElement.innerText = profitPerClick;
    profitPerSecondElement.innerText = profitPerSecond;

    toggleBoostsAvailability();
    toggleArmorsAvailability();
    toggleWeaponsAvailability();
    toggleBossesAvailability();
}


function addPointsFromClick() {
    currentScore += profitPerClick;
    recordScore += profitPerClick;

    updateUiScore();
}

function addPointsFromSecond() {
    currentScore += profitPerSecond;
    recordScore += profitPerSecond;

    updateUiScore();
}

function addPointsToScore() {
    $.ajax({
        url: '/score',
        method: 'post',
        dataType: 'json',
        async: false,
        data: { clicks: clicks, seconds: seconds },
        success: (response) => onAddPointsSuccess(response),
    });
}

function onAddPointsSuccess(response) {
    seconds = 0;
    clicks = 0;

    updateScoreFromApi(response);
}

function toggleBoostsAvailability() {
    const boostButtons = document.getElementsByClassName("boost-button");

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        const boostPriceElement = boostButton.getElementsByClassName("boost-price")[0];
        const boostPrice = Number(boostPriceElement.innerText);

        if (boostPrice > currentScore) {
            boostButton.disabled = true;
            continue;
        }

        boostButton.disabled = false;
    }
}
function toggleArmorsAvailability() {
    const armorButtons = document.getElementsByClassName("armor-button"); // Исправлено на armor-button

    for (let i = 0; i < armorButtons.length; i++) {
        const armorButton = armorButtons[i];

        const armorPriceElement = armorButton.getElementsByClassName("armor-price")[0];
        const armorPrice = Number(armorPriceElement.innerText);

        if (armorPrice > currentScore) {
            armorButton.disabled = true;
            continue;
        }

        armorButton.disabled = false;
    }
}

function toggleWeaponsAvailability() {
    const weaponButtons = document.getElementsByClassName("weapon-button"); // Исправлено на armor-button

    for (let i = 0; i < weaponButtons.length; i++) {
        const weaponButton = weaponButtons[i];

        const weaponPriceElement = weaponButton.getElementsByClassName("weapon-price")[0];
        const weaponPrice = Number(weaponPriceElement.innerText);

        if (weaponPrice > currentScore) {
            weaponButton.disabled = true;
            continue;
        }

        weaponButton.disabled = false;
    }
}


function toggleBossesAvailability() {
    const bossButtons = document.getElementsByClassName("boss-button");
    /*currentPower = document.getElementById("current_power");
    currentProtection = document.getElementById("current_protection");
    totalStats = Number(currentPower.innerText) + Number(currentProtection.innerText);*/

    for (let i = 0; i < bossButtons.length; i++) {
        const bossButton = bossButtons[i];
        const isPurchased = bossButton.dataset.isPurchased;
        const bossPriceElement = bossButton.getElementsByClassName("boss-price")[0];
        const bossPrice = Number(bossPriceElement.innerText);

        if (bossPrice > totalStats) {
            bossButton.disabled = true;
            continue;
        }


        bossButton.disabled = false;
    }
}


function buyBoss(bossButton) {
    const bossIdElement = bossButton.getElementsByClassName("boss-id")[0];
    const bossId = bossIdElement.innerText;

    const bossPriceElement = bossButton.getElementsByClassName("boss-price")[0];
    const bossPrice = Number(bossPriceElement.innerText);
    /*currentPower = document.getElementById("current_power");
    currentProtection = document.getElementById("current_protection")*/
    /*totalStats = Number(currentPower.innerText) + Number(currentProtection.innerText);*/

    if (totalStats < bossPrice) {
        alert("Недостаточно силы и защиты для победы наж этим боссом!");
        return;
    }

    $.ajax({
        url: '/boss/buy',
        method: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify({ bossId: bossId }),
        success: (response) => onBuyBossSuccess(response, bossButton),
        error: (xhr, status, error) => {
            console.error("Ошибка запроса:", error, xhr.responseText);
        },
    });

}



function onBuyBossSuccess(response, bossButton) {
    const score = response["score"];
    const bossImageBase64 = response["imageBoss"];


    const bossPriceElement = bossButton.getElementsByClassName("boss-price")[0];
    const bossPrice = Number(response["price"]);

    bossPriceElement.innerText = bossPrice;

    bossButton.dataset.isPurchased = "true";
    bossButton.disabled = true;

    updateScoreFromApi(score);
}


