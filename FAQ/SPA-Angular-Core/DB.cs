using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SPA_Angular_Core.Models;

namespace SPA_Angular_Core
{
    public class DB 
    {
        private readonly KundeContext _context;
        public DB(KundeContext context)
        {
            _context = context;
        }


        //METODER FOR FAQ
        
        //Hente ut alle spørsmål og svar(FAQ)
        public List<DomeneFAQ> hentAlleFAQ()
        { 
            List<DomeneFAQ> alleFAQ = _context.FAQS.Select(k=> new DomeneFAQ()
                                      {
                                          id = k.id,
                                          kategori = k.kategori,
                                          spmTekst = k.spmTekst,
                                          svarTekst = k.svarTekst,
                                          likerKlikk = k.likerKlikk
            }).OrderByDescending(o=>o.likerKlikk).
                                      ToList();
            return alleFAQ;
        }


        //Lagre økningen ved å øke antall likerklikk
        public bool lagreAntallLikerKlikk(DomeneFAQ innLikerKlikk)
        {

            var oppdaterLikerKlikk = new DomeneFAQ() 
            {
                id = innLikerKlikk.id,
                likerKlikk = innLikerKlikk.likerKlikk
            };

            Models.FAQ funnetVerdi = _context.FAQS.FirstOrDefault(c => c.id == oppdaterLikerKlikk.id);

            funnetVerdi.likerKlikk = oppdaterLikerKlikk.likerKlikk; //Vi oppdaterer likerKlikk i db til å bli den verdien som blir sendt inn i parameteren.


            try
            {
                //Vi lagrer den nye verdien. 
                _context.SaveChanges();
            }
            catch (Exception feil)
            {
                return false;
            }
            return true;
        }


















        //METODER FOR NYE SPØRSMÅL SOM BLE LAGT TIL

        //Lagre nye spm
        public bool lagreNyttSpm(NyeSpm innSpm)
        {
            var nyttSpm = new nyeSpm
            {
                navn = innSpm.navn,
                tlf = innSpm.tlf,
                epost = innSpm.epost,
                spmTekst = innSpm.spmTekst,
                kategoriForNyeSpm = innSpm.kategoriForNyeSpm


            };

            try

            {
               //Vi lagrer det stilte spørsmålet med kundeinfo til databasen
               _context.nyeSpm.Add(nyttSpm);
               _context.SaveChanges();
            }
            catch(Exception feil)
            {
                return false;
            }
            return true;
        }


        //Hente ut alle spørsmål og svar
        public List<NyeSpm> hentAlleNyeSpm()
        {
            List<NyeSpm> alleNyeSpm = _context.nyeSpm.Select(v => new NyeSpm()
                {
                    navn = v.navn,
                    tlf = v.tlf,
                    epost = v.epost,
                    spmTekst = v.spmTekst,
                    kategoriForNyeSpm = v.kategoriForNyeSpm
            }).
                ToList();

            return alleNyeSpm;
        }






    }
}