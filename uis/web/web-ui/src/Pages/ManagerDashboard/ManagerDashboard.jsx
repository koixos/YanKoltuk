import { Link } from "react-router-dom";
import "./ManagerDashboard.css"
import { useState } from 'react';

function ManagerDashboard() {
    const [cards] = useState([
        {
            title: "Servis Ekle",
            text: `Sisteme bir servis kaydet.`,
            link: "/add-service"
        }, {
            title: "Servis Listesi",
            text: `Kayıtlı tüm servisleri görüntüle.`,
            link: "/view-services"
        }, {
            title: "Servis Raporları",
            text: `Servislerin hareket detaylarını görüntüle.`,
            link: "/service-logs"
        },
    ]);

    return (
        <div className="home">
            <section className="section">
                <div className="container">
                    <h1 className="welcome">Yan Koltuk'a Hoş Geldiniz!</h1>
                    <div className="cards">
                        {
                            cards.map((card, i) => (
                                <div key={i} className='card'>
                                    <h3>
                                        { card.title }
                                    </h3>
                                    <p className="description">
                                        { card.text }
                                    </p>
                                    <Link to={card.link} style={{ textDecoration: 'none' }}>
                                        <button className='btn'>Seç</button>
                                    </Link>
                                </div>
                            ))
                        }
                    </div>
                </div> 
            </section> 
        </div>
    );
}

export default ManagerDashboard;