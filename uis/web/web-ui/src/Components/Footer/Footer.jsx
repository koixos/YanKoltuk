import React from 'react';
import './Footer.css';

const Footer = () => {
  return (
    <footer id='footer' class="text-center text-white">
        <div class="container" id='container'>
            <section class="mt-5">
                <div class="row text-center d-flex justify-content-center pt-5">
                    <div class="col-md-2">
                        <h6 class="text-uppercase font-weight-bold">
                            <a href="#!" class="text-white">About us</a>
                        </h6>
                    </div>

                    <div class="col-md-2">
                        <h6 class="text-uppercase font-weight-bold">
                            <a href="#!" class="text-white">Help</a>
                        </h6>
                    </div>

                    <div class="col-md-2">
                        <h6 class="text-uppercase font-weight-bold">
                            <a href="#!" class="text-white">Contact</a>
                        </h6>
                    </div>
                </div>
            </section>

            <hr class="my-5" />

            <section class="mb-5">
                <div class="row d-flex justify-content-center">
                    <div class="col-lg-8">
                        <p>
                        Müşterilerimize en iyi hizmeti sunmayı hedefliyoruz.
                        Bu uygulama, servis süreçlerinizi kolaylaştırmak, güncel bilgilerle donatmak ve size güvenilir bir takip deneyimi sunmak için geliştirildi.
                        Tüm servis kayıtlarınız şeffaf bir şekilde elinizin altında. Herhangi bir sorun yaşarsanız, 7/24 destek ekibimizle iletişime geçebilirsiniz.
                        Güveniniz için teşekkür ederiz!
                        </p>
                    </div>
                </div>
            </section>

            <section class="text-center mb-5">
                <a href="" class="text-white me-4">
                    <i class="fab fa-facebook-f"></i>
                </a>
                <a href="" class="text-white me-4">
                    <i class="fab fa-twitter"></i>
                </a>
                <a href="" class="text-white me-4">
                    <i class="fab fa-google"></i>
                </a>
                <a href="" class="text-white me-4">
                    <i class="fab fa-instagram"></i>
                </a>
                <a href="" class="text-white me-4">
                    <i class="fab fa-linkedin"></i>
                </a>
                <a href="" class="text-white me-4">
                    <i class="fab fa-github"></i>
                </a>
            </section>
        </div>

        <div id='copyright' class="text-center p-3">
            © 2025 Copyright: 
            <a class="text-white" href="https://mdbootstrap.com/">YanKoltuk.com.tr</a>
        </div>
    </footer>
  )
}

export default Footer;