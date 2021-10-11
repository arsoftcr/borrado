var entidad = document.querySelector('#entidades');
var borrarbtn = document.querySelector('#borrarbtn');
var detenerbtn = document.querySelector('#detenerbtn');
var publica = document.querySelector('#publica');
var privada = document.querySelector('#privada');
var idnum = document.querySelector('#idnum');


detenerbtn.addEventListener('click', async (e) => {
    e.preventDefault();
    e.target.style.backgroundColor = 'green';
    await detenerFlag();
    e.target.style.backgroundColor = '#2840a1';
});

borrarbtn.addEventListener('click', async (e) => {
    e.preventDefault();
    if (entidad.value !== '' && idnum.value !== ''
        && publica.value !== '' && privada.value !== '') {
        e.target.style.backgroundColor = 'green';
        await borrado(publica.value, privada.value, entidad.value, idnum.value);
        e.target.style.backgroundColor = '#2840a1';
        detenerbtn.style.backgroundColor = '#2840a1';
    } else {
        alert('faltan datos');
    }
  
});





async function borrado(ppublica,pprivada,entidad,id) {

    try {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Accept", "*/*");

        var raw = JSON.stringify(
            {
                "EntidadString": entidad,
                "Id": id,
                "Payload": {
                    "username": ppublica,
                    "password": pprivada
                }

            });

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
        };
        let request = await fetch(`/Home/Borrar`, requestOptions);

        let response = request.json();

        console.log(response);
    } catch (error) {

    }
}


async function detenerFlag() {

    try {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        myHeaders.append("Accept", "*/*");

        var raw = "";

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
        };
        let request = await fetch(`/Home/Detener`, requestOptions);

        let response = request.json();

        console.log(response);
    } catch (error) {

    }
}
