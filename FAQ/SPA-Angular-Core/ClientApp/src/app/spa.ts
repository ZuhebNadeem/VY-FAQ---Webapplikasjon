import {Component, OnInit} from "@angular/core";
import { Http, Response } from '@angular/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import "rxjs/add/operator/map";
import {FAQ} from "./FAQ";
import { Headers } from "@angular/http";
import { nyeSpm } from "./nyeSpm";


@Component({
  selector: "min-app",
  templateUrl: "SPA.html"
})
export class SPA {

  //For FAQ
  visFAQ: boolean;
  laster: boolean; //det vises i starten og blir overskrevet
  visAntallLikes: boolean;
  listAlleFAQ: Array<FAQ> = [];

  //Array for FAQ kategoriene
  listBillettFAQ: Array<FAQ> = [];
  listAvgangFAQ: Array<FAQ> = [];
  listAnnetFAQ: Array<FAQ> = [];


  //For nye spørsmål
  skjema: FormGroup;
  visSkjema: boolean;
  visNyeSpm: boolean;
  listNyeSpm: Array<nyeSpm> = [];


  //Regexhåndtere feltene man skriver nye spørsmål inn i.
  constructor(private _http: Http, private fb: FormBuilder) {
    this.skjema = fb.group({
      id: [""],
      navn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
      tlf: [null, Validators.compose([Validators.required, Validators.pattern("[0-9]{8}")])],
      epost: [null, Validators.compose([Validators.required, Validators.pattern("[^@]+@[A-Za-z]+[.]+[A-Za-z]{2,30}")])],
      sporsmol: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-.? ]{2,30}")])],
      kategoriForNyeSpm: [null, Validators.required],

    });
  }


  //Ved start, hva jeg vil skal vises/ikke vises og hva som skal kjøres
  ngOnInit() {

    //FAQ 
    this.laster = true;
    this.hentAlleFAQ();
    this.visFAQ = true;
    this.visAntallLikes = true;

    //Nye spørsmål
    this.visSkjema = false;
    this.visNyeSpm = false;
  }


  //METODER FOR FAQ


  //Henter alle spørsmål og svar fra FAQ
  hentAlleFAQ() {
    this._http.get("api/Home/")
      .map(returData => {

        let JsonData = returData.json();
        return JsonData;
      })
      .subscribe(
        JsonData => {
          if (JsonData) {
            this.listAlleFAQ = JsonData;
            this.laster = false;
            for (let verdi of this.listAlleFAQ) {

              if (verdi.kategori == "Billett") {
                this.listBillettFAQ.push(verdi);
              }

              if (verdi.kategori == "Annet") {
                this.listAnnetFAQ.push(verdi);
              }

              if (verdi.kategori == "Avgang") {
                this.listAvgangFAQ.push(verdi);
              }
            }
          }
        },
        error => alert(error),
        () => console.log("Vi klarte å liste ut FAQ")
      );
  };


  //Lagrer de nye verdiene, når man øker antall like
    TommelOpp(id: number) {
    for (let x of this.listAlleFAQ) {
      if (x.id == id) {
          x.likerKlikk++;


          const nyVerdi = new FAQ();
          nyVerdi.id = id;
          nyVerdi.likerKlikk = x.likerKlikk; 


          var body: string = JSON.stringify(nyVerdi);
          var headers = new Headers({ "Content-Type": "application/json" });

          this._http.put("api/Home", body, { headers: headers })
            .subscribe(
                () => {

                  console.log("Klarte å øke antall liker-klikk ");
              });

        }
    }
  }


//Lagrer de nye verdiene, når man øker antall unlike
  TommelNed(id: number) {
    for (let x of this.listAlleFAQ) {
      if (x.id == id) {
          x.likerKlikk-=1;


        const nyVerdi = new FAQ();
        nyVerdi.id = id;
        nyVerdi.likerKlikk = x.likerKlikk;


        var body: string = JSON.stringify(nyVerdi);
        var headers = new Headers({ "Content-Type": "application/json" });

        this._http.put("api/Home", body, { headers: headers })
          .subscribe(
              () => {

                console.log("Klarte å øke antall ikke liker-klikk ");
            });

      }

    }
}


  //NYE SPØRSMÅL METODER


    //Viser spørsmål som allerede er stilt
    visStilteSpm() {
      this.hentAlleNyeSpm();
      this.visSkjema = false;
      this.visFAQ = false;
      this.visNyeSpm = true;
    }

    //Vise forsiden
    Forside() {
      this.laster = false;
      
      this.visFAQ = true;
      this.visAntallLikes = true;

      //Nye spørsmål
      this.visSkjema = false;
      this.visNyeSpm = false;

    }


    //Viser skjemaet for registrering av nytt spørsmål
    visNySpmSkjema() {

      // må resette verdiene i skjema dersom skjema har blitt brukt til endringer
      this.skjema.setValue({
        id: "",
        navn: "",
        tlf: "",
        epost: "",
        sporsmol: "",
        kategoriForNyeSpm: "Priser"

      });

        //Velger hva som skal bli true og false når vi viser skjemaet
        this.skjema.markAsPristine();
        this.visSkjema = true;
        this.visFAQ = false;
        this.visNyeSpm = false;

    }



    //Når man submitter etter å ha skrevet inn riktig verdier i feltene 
    vedSubmit() {
      this.lagreNyttSpm();
    }


    //Når man trykker tilbake knappen i input-felt skjema visningen
     tilbakeTilFAQListe() {
      this.visFAQ = true;
      this.visSkjema = false;
      this.visAntallLikes = true;
    }





    //Lagrer det nye spørsmålet og kundeinfo
    lagreNyttSpm() {

      var lagreSpm = new nyeSpm();

        lagreSpm.navn = this.skjema.value.navn;
        lagreSpm.tlf = this.skjema.value.tlf;
        lagreSpm.epost = this.skjema.value.epost;
        lagreSpm.spmTekst = this.skjema.value.sporsmol;
        lagreSpm.kategoriForNyeSpm = this.skjema.value.kategoriForNyeSpm;

        



        var body: string = JSON.stringify(lagreSpm);
        var headers = new Headers({ "Content-Type": "application/json" });

      this._http.post("api/Home", body, { headers: headers })
        .subscribe(
            () => {
              this.hentAlleNyeSpm();
              this.visSkjema = false;
              this.visFAQ = false;
              this.visNyeSpm = true;
              },
          error => alert(error),
          () => console.log("Spørsmålet ble lagret")
        );
    };



    //Henter alle nye spørsmål som ble spurt
    hentAlleNyeSpm() {
      this._http.get("api/NyeSpm/")
        .map(returData => {
          let JsonData = returData.json();
          return JsonData;
        })
        .subscribe(
          JsonData => {
            if (JsonData) {
                this.listNyeSpm = JsonData;
                this.visSkjema = false;
            }

          },
          error => alert(error),
          () => console.log("Klarte å hente ut spørsmålene")
        );
    };








}
