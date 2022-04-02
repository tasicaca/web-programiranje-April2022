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
                if (p.ok) {/*
                    fetch("https://localhost:5001/Ispit/UcitavanjeTriTopFilmaKategorije/" + izabranaKategorija + "/" + this.id, {
                        method: "GET",
                    }).then(p => {
                        p.json().then(d => {
                             
                               var divZaOcene=this.kontejner.querySelector(".divZaCrtanjeOcena");
                               if (divZaOcene!=null)
                                { 
                                   this.kontejner.removeChild(divZaOcene);
                                }
                                var divZaCrtanjeOcena = document.createElement("div");
                                divZaCrtanjeOcena.className = "divZaCrtanjeOcena";
                                this.kontejner.appendChild(divZaCrtanjeOcena);

                                var film1 =new Film(d[0].id, d[0].ime, d[0].ocena,d[0].brojOcena);
                                var film2 =new Film(d[1].id, d[1].ime, d[1].ocena,d[1].brojOcena);
                                var film3 =new Film(d[2].id, d[2].ime, d[2].ocena,d[2].brojOcena);
                                film1.crtajFilm(divZaCrtanjeOcena);
                                film2.crtajFilm(divZaCrtanjeOcena);
                                film3.crtajFilm(divZaCrtanjeOcena);

                            })
                        })*/
                    alert("Kupljena je ploca");
                } else {
                    alert("Greška prilikom upisa.");
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
            })
            }
        }
    }
}