import {Sara } from "./Sara.js";
import {Ploca} from "./Ploca.js"
export class Prodavnica {
   
    constructor(id, naziv) {
        this.id = id;
        this.naziv = naziv;
        this.kontejner = null;
        this.sare = [];
        this.ploce=[];
    }

    crtajPK(host) {

        if (!host) {
            throw new Exception("Roditeljski element ne postoji");
        }
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("kontejner");
        host.appendChild(this.kontejner);
        this.crtajFormu(this.kontejner);
    }
    crtajFormu(host) {
        const kontForma = document.createElement("div");
        kontForma.className = "kontForma";
        host.appendChild(kontForma);

        var divZaIzborKategorije = document.createElement("div");

        const kontForma1 = document.createElement("div");
        kontForma1.className = "kontForma1";
        kontForma.appendChild(kontForma1);

        let labela = document.createElement("label");
        labela.innerHTML = "Sara"
        divZaIzborKategorije.appendChild(labela);
        kontForma1.appendChild(divZaIzborKategorije);

        var sel = document.createElement("select");
        sel.name = "selectSara";
        let labela1 = document.createElement("label");

        divZaIzborKategorije.appendChild(labela1);
        divZaIzborKategorije.appendChild(sel);
        kontForma1.appendChild(divZaIzborKategorije);

        fetch("https://localhost:5001/Ispit/PreuzmiSare/" + this.id, {
            method: "GET",
        }).then(p => {
            p.json().then(data => {
                data.forEach((dataa) => {
                    var opcija = document.createElement("option");
                    opcija.innerHTML = dataa.naziv;
                    opcija.value = dataa.id;
                    sel.appendChild(opcija);
                })
            })
        })

        
        var elLabela = document.createElement("label");
        elLabela.innerHTML = "duzina";
        kontForma.appendChild(elLabela);

        var inputDuzina = document.createElement("input");
        inputDuzina.className = "duzina";
        kontForma.appendChild(inputDuzina);

        var elLabelaSirina = document.createElement("label");
        elLabelaSirina.innerHTML = "sirina";
        kontForma.appendChild(elLabelaSirina);

        var inputSirina = document.createElement("input");
        inputSirina.className = "sirina";
        kontForma.appendChild(inputSirina);

        const dugme = document.createElement("button");
        dugme.innerHTML = "Dodaj ocenu";
        kontForma.appendChild(dugme);

        dugme.onclick = (ev) => {
            var duzina = parseInt(this.kontejner.querySelector(".duzina").value);
            var sirina = parseInt(this.kontejner.querySelector(".sirina").value);
            if ((sirina>10) || (duzina<0))
                alert ("Neispravna vrednost ocene van opsega");
            else {

            var idSare = this.kontejner.querySelector('select[name="selectSara"]').value;
         
            fetch("https://localhost:5001/Ispit/IzmenaPodatakaOPloci/" + duzina + "/" + sirina + "/" + idSare + "/" + this.id , {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                
            }).then(p => {
                if (p.ok) {
                   
                    alert("Kupljena je ploca");
                } else {
                    alert("Greška prilikom upisa, Najverovatnije dimenzija ploce nije dostupna.");
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
            })
            }
        }
    }
}