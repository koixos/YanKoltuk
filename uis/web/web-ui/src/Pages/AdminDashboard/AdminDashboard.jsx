import { Link } from "react-router-dom";
import "./AdminDashboard.css"
import { useState } from 'react';

function AdminDashboard() {
    const [cards] = useState([
        {
            title: "Yönetici Ekle",
            text: `Sisteme bir yönetici kaydet`,
            link: "/add-manager"
        }, {
            title: "Tüm Yöneticiler",
            text: `Kayıtlı tüm yöneticilerin listesini görüntüle`,
            link: "/view-managers"
        },
    ]);

    return (
        <div className="home">
            <section className="section">
                <div className="container">
                    <h1 className="welcome">Yan Koltuk'a Hoşgeldiniz!</h1>
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

export default AdminDashboard;