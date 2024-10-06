import { Link } from "react-router-dom";
import "./Home.css"
import { useState } from 'react';

function Home() {
    const [cards] = useState([
        {
            title: "Add Service",
            text: `Register a service to the system.`,
            link: "/add-service"
        }, {
            title: "View Services",
            text: `View the registered services as a list.`,
            link: "/view-services"
        }, {
            title: "View Service Logs",
            text: `View the movements of the registered services.`,
            link: "/service-logs"
        },
    ]);

    return (
        <div className="home">
            <section className="section">
                <div className="container">
                    <h1 className="welcome">Welcome to Yan Koltuk!</h1>
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

export default Home;