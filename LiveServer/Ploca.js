export class Ploca {
    constructor(id, naziv, duzina, sirina, otpadna) {
        this.id = id;
        this.naziv = naziv;
        this.duzina = duzina;
        this.sirina=sirina;
        this.inicijalnaDuzina=2;
        this.incijalnaSirina=1;//mora dodati zbog izbacivanja otpadaka od 20 posto
        this.otpadna=otpadna;
        this.miniKontejner1 = null;
        this.miniKontejner0= null;
       
    }
}

