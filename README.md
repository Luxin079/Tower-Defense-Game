# Tower-Defense-Game

Naam: Mads 

Klas: 2A

Datum: 9/8/2025

## 1. Titel en elevator pitch
Titel: Dash Defense

Elevator pitch, maximaal twee zinnen:
Beschrijf kort wat jouw game is en waarom het leuk is om te spelen.

Mijn game is een fast paced stragische tower defense game met een simpele art style en goeie mechanics

## 2. Wat maakt jouw tower defense uniek
Beschrijf in één of twee zinnen wat jouw game onderscheidt van een standaard tower defense. Denk aan iets dat de speler op een nieuwe manier laat nadenken of spelen.

Wat mijn tower defense game anders maakt dan andere is de old school art style en de manier hoe towers kunnen samenwerken met elkaar met vershillende mechanics.

## 3. Schets van je level en UI
Maak een schets op papier of digitaal en voeg deze afbeelding toe aan je repository. Voeg in deze sectie de afbeelding in.

(afbeelding)

Je schets bevat minimaal:
1. Het pad waar de vijanden over lopen met beginpunt en eindpunt.
2. De plaatsen waar torens gebouwd kunnen worden.
3. De locatie van de basis of goal die verdedigd moet worden.
4. De UI onderdelen geld, wave teller, levens, startknop en pauzeknop.
5. Een legenda met symbolen of kleuren voor torens, vijanden, pad, basis en UI.

## 4. Torens
Toren 1 naam = tripolio, bereik = medium, schade = medium, unieke eigenschap = schiet 3 kogels per keer.

Toren 2 naam = staralia, bereik = close schade = mediun, unieke eigenschap = gooit 2 sterretjes omstebuurt die ook weer terug komen.

Eventuele extra torens:

## 5. Vijanden
Cubeangolo, slow speed, levens =  veel, speciale eigenschap = niks

Triangolo, medium speed, levens = medium, speciale eigenschap = versneld wanneer het geraakt word door een tower.

Staralio, high speed, Levens = weinig, speciale eigenschap = elke enemy die hij aanraakt krijgt meer levens erbij



## 6. Gameplay loop
Beschrijf in drie tot vijf stappen wat de speler steeds doet.
1. towers kopen

2. towers plaatsen

3. towers upgraden / combineren 


## 7. Progressie
Leg uit hoe het spel moeilijker wordt naarmate de waves doorgaan. Denk aan sterkere vijanden, kortere tussenpozen, hogere kosten of lagere beloningen.

hoe meer waves je overleeft heeft invloed op hp van de enemys en hoe veel verschillende enemys er spawnen je zal ook je geld na elke wave slimmer moeten gebruiken omdat de enemys moeilijker en meer overwachts worden. dus je moet goed nadenken over welke towers en buffs welke enemys counteren.

## 8. Risico’s en oplossingen volgens PIO
- Probleem 1: backround en ui laten werken
- Impact: het visuele deel van de game
- Oplossing: goed de camara gebruiken en simpele code gebruiken

- Probleem 2: towers met elkaar samen laten werken
- Impact: core game mechanic
- Oplossing: variebles goed gebruiken


- Probleem 3: geld systeem
- Impact: gameplay
- Oplossing: goede berekeningen maken
  
## 9. Planning per sprint en mechanics
Schrijf per sprint welke mechanics jij oplevert in de build. Denk aan voorbeelden zoals vijandbeweging over een pad, torens plaatsen, doel kiezen en schieten, waves starten, UI voor geld en levens, upgrades, jouw unieke feature.

Sprint 1 mechanics: art van enemys, towers en de map + simpele basis code om enemys en towers iets te laten doen

Sprint 2 mechanics: art van ui + ui kunnen gebruiken in de game + een werkende test versie van de game

Sprint 3 mechanics: uitgebreide fucnties van enemys en towers + gameplay zo leuk mogelijk maken + game afmaken




## 10. Inspiratie
Noem een bestaande tower defense game die jou inspireert en wat je daarvan meeneemt of juist vermijdt.

Bloons td 6 en onslaught 2

## 11. Technisch ontwerp mini

Lees dit korte voorbeeld en vul daarna jouw eigen keuzes in.

Voorbeeld ingevuld bij 11.1 Vijandbeweging over het pad
- Keuze:
Vijanden volgen punten A, B, C en daarna de goal.
- Risico:
Een vijand loopt een punt voorbij of blijft hangen.
- Oplossing:
Als de vijand dichtbij genoeg is kiest hij het volgende punt. Bij de goal gaat één leven omlaag en verdwijnt de vijand.
- Acceptatie:
Tien vijanden lopen van start naar de goal zonder vastlopers en verbruiken elk één leven.
Alle tien vijanden bereiken achtereenvolgens elk waypoint binnen één seconde na elkaar.

### 11.1 Vijandbeweging over het pad
- Keuze: enemys volgen recht het pad met mogelijk uitzonderingen.
- Risico: het goed laten spawmen van emeys
- Oplossing: code goed van verschillende scripts goed met elkaar latne samenwerken
- Acceptatie: enemys die goed kunnen spawnen en zonder problemen door de map gaan


### 11.2 Doel kiezen en schieten
- Keuze: towers die je kan plaatsen die schieten op enemys
- Risico: probleem als er meerdere towers actief zijn
- Oplossing: 
- Acceptatie:

### 11.3 Waves en spawnen
- Keuze: na elke waves spawnen er meer en sterkeren enemys
- Risico: 
- Oplossing:
- Acceptatie:

  
### 11.4 Economie en levens
- Keuze: je krijgt geld als je tower enemys killen en aan het eind van elke wave. je verlies levens elke keer als een enemy het eiende bereikt
- Risico: kans op problemen met het laten samenwerken van mechanics
- Oplossing: simpele herbruikbare code gebruiken
- Acceptatie:

### 11.5 UI basis
- Keuze: simpele art en makkelijk te begrijpen
- Risico: niks
- Oplossing:
- Acceptatie:
