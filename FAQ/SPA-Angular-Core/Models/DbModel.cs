using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SPA_Angular_Core.Models
{

    //Spm og Svar
    public class FAQ
    {
        [Key] public int id { get; set; }
        public string kategori { get; set; }
        public string spmTekst { get; set; }
        public string svarTekst { get; set; }
        public int likerKlikk { get; set; }


    }


    //Nye spørsmål som brukeren sender inn
    public class nyeSpm
    {
        [Key] public int id { get; set; }
        public string navn { get; set; }
        public string tlf { get; set; }
        public string epost { get; set; }
        public string spmTekst { get; set; }
        public string kategoriForNyeSpm { get; set; }

    }


    public class KundeContext : DbContext
    {
        public KundeContext(DbContextOptions<KundeContext> options)
            : base(options)
        {
        }


        //Oppretter to tabeller i databasen vår

        public DbSet<FAQ> FAQS { get; set; }

        public DbSet<nyeSpm> nyeSpm { get; set; }



        //Seed verdier, de verdiene som er der fra start(FAQ verdier)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FAQ>().HasData(
                new FAQ
                {
                    id = 1,
                    kategori = "Annet",
                    spmTekst = "Hvor kan jeg kontakte kundesenteret deres?",
                    svarTekst = "Ring 21397318",
                    likerKlikk = 5
                },
                new FAQ
                {
                    id = 2,
                    kategori = "Billett",
                    spmTekst = "Hvilken type billetter har dere?",
                    svarTekst = "Voksen, Barn, Honnør"
                },
                new FAQ
                {
                    id = 3,
                    kategori = "Billett",
                    spmTekst = "Jeg kjøpte feil billett, hva gjør jeg?",
                    svarTekst = "Kontakt kundesenteret på 21397318"

                },
                new FAQ
                {
                    id = 4,
                    kategori = "Avgang",
                    spmTekst = "Hvor kan jeg se når togene har avgang?",
                    svarTekst = "På vår hjemmeside eller i appen vår"

                },
                new FAQ
                {
                    id = 5,
                    kategori = "Billett",
                    spmTekst = "Hvor mye koster en månedsbillet for 1 sone?",
                    svarTekst = "For sone 1 gjelder disse prisene: Voksen for 600kr, Barn og Honnør for 300kr",
                    likerKlikk = 2

                },
                new FAQ
                {
                    id = 6,
                    kategori = "Avgang",
                    spmTekst = "Kjører toget hele tiden?",
                    svarTekst = "Nei. 05.25 kjøres første toget og siste kjører 01.41",
                    likerKlikk = -4
                },
                new FAQ
                {
                    id = 7,
                    kategori = "Billett",
                    spmTekst = "Hvor kan jeg kjøpe billett?",
                    svarTekst = "Du kan kjøpe billett på våre automater, hjemmeside eller i vår app på mobil"

                },
                new FAQ
                {
                    id = 8,
                    kategori = "Avgang",
                    spmTekst = "Hvilket rettigheter har jeg om toget er innstilt?",
                    svarTekst = "Du kan få refusjon av billettprisen du har kjøpt"

                },
                new FAQ
                {
                    id = 9,
                    kategori = "Annet",
                    spmTekst = "Kan jeg ta med en kjæledyr på reisen?",
                    svarTekst = "Ja"

                },
                new FAQ
                {
                    id = 10,
                    kategori = "Annet",
                    spmTekst = "Har dere tog til Gøteborg?",
                    svarTekst = "Vi har tog og buss for tog sponset av VY"

                }

            );

        }
    }
}