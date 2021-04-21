//scripts.js isimli javascript dosyası/sayfası
// Menü responsive açma butonu scripti
const selectElement =(element)=> document.querySelector(element);
selectElement('.menu-icons').addEventListener('click', () =>{  //tuşa basınca menu icon sınıfı aktifleşiyor böylece nav sınıfıda cssde yaptığım değişiklikler çalışıyor
selectElement( 'nav').classList.toggle('active');
});

var mybutton = document.getElementById("myBtn");         //en yukarı çıkarma kodu kaynak w3school scroll html.
        window.onscroll = function() {scrollFunction()};
        function scrollFunction() {
          if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            mybutton.style.display = "block";
          } else {
            mybutton.style.display = "none";
          }
        }
        function topFunction() {
          document.body.scrollTop = 0;
          document.documentElement.scrollTop = 0;
        }


// bu işin ana kısmı div'in id si ile editlenebilir yap diyorum ve düzenle butonuna 2 defa basılmaması için disable yapıyorum...kaydet butonunda o yüzden tekrar geri aktif leştiriyorum
        function makeEditable(divhak){
            divhak.style.border ="2px solid #69FCA1";
            divhak.contentEditable =true; 

            if(divhak.contentEditable=true){
                document.getElementById("buttontoedit").disabled = true;	
            }
            var b =document.getElementById("kaydet-btn");
            if (b.style.display === "none") {
              b.style.display = "inline-block";
            } else {
              b.style.display = "none";
            }
            
        }



//kaydet butonuna bastıktan sonra düzenleme butonunu tekrar aktifleştirip ve div üzerindeki editi kaldırıyor.
        function gizle() {
            var x = document.getElementById("buttontoedit");
            var b =document.getElementById("kaydet-btn");
            if (b.style.display === "inline-block") {
              b.style.display = "none";
            } else {
              b.style.display = "inline-block";
            }
            document.getElementById("buttontoedit").disabled = false;
            document.getElementById("divhak").contentEditable =false;
            document.getElementById("divhak").style.border ="none";
          }
// bu ise ilk düzenleme butonuna basınca gizli olan display none olan kaydet butonunu çıkarıyor 
        




        








