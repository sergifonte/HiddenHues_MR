# Pràctica de Realitat Mixta - HiddenHues_MR

Aquest repositori conté el projecte base en Unity desenvolupat com a fonament arquitectònic per a un prototip de joc en Realitat Mixta interactiva dirigit a les ulleres **Meta Quest 3**.

Seguint els requeriments de l'entrega, aquesta versió se centra exclusivament a consolidar les mecàniques nuclears (*core loops*) i un flux de dades robust mitjançant programació lògica, prescindint de qualsevol tipus d'art intermedi, *polish* o disseny estètic superficial.

---

## 🎯 Objectius del Lliurament

L'objectiu d'aquest lliurament és demostrar un entorn de treball funcional on el loop de joc es pugui executar de manera cíclica, interactiva i lliure d'errors de memòria o conflictes d'interfície:
* **Validació inicial del Setup:** Verificació de precondicions físiques per a l'inici del nivell.
* **Loop de Joc Operatiu:** Ordre aleatòria -> Resposta física de l'usuari -> Resolució de lògica.
* **Flux de UI Segur:** Gestió dels botons, menús i fletxes de navegació unidireccionals sense duplicació d'esdeveniments.

---

## 🛠️ Mecàniques Fonamentals Implementades

### 1. Loop Jugable Central (The Cube Game)
* **Detecció de Presència (`TableZone`):** Una zona virtual tridimensional vinculada a la taula real actua com a *Trigger* de físiques per verificar si es compleixen els requisits per començar.
* **Validació Mínima d'Elements:** El joc només s'inicia si detecta de forma activa, com a mínim, un cub de cada color a la zona de joc (Vermell, Groc, Blau, Verd).
* **Ordre Dinàmica i Avaluació de l'Estat:** El sistema assigna una tasca aleatòria modificant l'estat textual ("LIFT THE CUBE"). Mitjançant l'esdeveniment `OnTriggerExit` (en aixecar el cub de la taula real a una alçada prefixada superior a `0.3m`), l'script avalua la correspondència del `colorID`. Emet una resposta visual de `CORRECT!` (passant a la següent ronda mitjançant corrutines) o `INCORRECT!`.

### 2. Arquitectura del Control d'Interfície (UI Flow)
* **Intercanvi Dinàmic de Navegació:** S'han separat els fluxos lògics de la UI per evitar col·lisió d'esdeveniments a la Hierarchy.
    * **Abans de jugar:** El botó "Go to Level" i la fletxa de retorn al menú principal de l'aplicació estan actius.
    * **En validar l'inici:** S'amaguen completament el botó i la fletxa del menú principal, i s'activa una fletxa de control dedicada en el mateix espai (`FletxaAturarJoc`).
    * **En aturar la partida:** La fletxa del joc restableix els estats globals, neteja els textos, deté les corrutines i reverteix la visualització dels botons inicials amb total seguretat.
* **Temporització d'Errors:** Per evitar la sobrecàrrega visual i textos congelats en pantalla, el missatge d'avís per falta de components físics ("MISSING CUBES") s'esborra automàticament de forma transparent mitjançant temporitzadors interns al cap de 4 segons.

---

## 🏗️ Estructura Tècnica de la Escena

El comportament del sistema es recolza sobre tres scripts clau dissenyats de forma modular:

* **`GameManagerMR.cs`**: El cervell del prototip. Gestiona els estats de la partida (`isGamePlaying`, `isWaitingForNextRound`), l'esborrat asíncron dels textos d'error i l'actualització forçada dels components `TextMeshProUGUI` i visibilitat d'objectes. Implementa el patró *Singleton* per seguretat.
* **`TableZone.cs`**: Controla les físiques en l'eix Y (alçada). Gestiona la llista d'objectes dinàmics rastrejats (`cubesOnTable`) en base als esdeveniments `OnTriggerEnter` i `OnTriggerExit`.
* **`TrackedCube.cs`**: Script atòmic adjunt a cadascun dels elements interactius de 6x6x6 cm. Conté les metadades d'identificació de color (`colorID`) utilitzades pel GameManager. Els objectes estan configurats com a `Is Kinematic` amb un component `Rigidbody` fixat.

---

## 💻 Entorn de Proves i Simulació

A causa de les restriccions d'accés per permisos de seguretat del compte de l'administrador del visor utilitzat durant el desenvolupament en entorns MacOS, el projecte s'ha lliurat preparat per a ser provat de manera híbrida:

1.  **A l'Editor de Unity (Simulador PC/Mac):** Es pot utilitzar el `XR Device Simulator` (inclòs al paquet *XR Interaction Toolkit*) per simular el moviment del cap i de les mans mitjançant teclat i ratolí (`W/A/S/D` + `Shift/Espai`), facilitant el testeig del 100% de la lògica descrita.
2.  **A les Meta Quest 3:** El projecte està lligat de forma nativa al perfil d'Android amb les configuracions exigides d'OpenXR (`Hand Tracking Submission` actiu en l'apartat de Meta).
