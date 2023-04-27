import React, { useEffect, useState } from "react";
import { OrphanageRequest } from "../types/OrphanageRequest";
import { FiClock, FiInfo } from "react-icons/fi";
import { Map, Marker, TileLayer } from "react-leaflet";
import { useParams } from 'react-router-dom';

import Sidebar from "../components/SideBar";
import mapIcon from "../utils/mapIcon";
import { FaWhatsapp, FaWhatsappSquare } from 'react-icons/fa';

import '../styles/pages/orphanage.css';
import axios, { AxiosRequestConfig } from "axios";
import { BASE_URL } from "../utils/requests";

type OrphanageParams = {
  orfanatoId: string;
}

function toPascalCase(text: string) {
  return text.replace(/(^\w|-\w)/g, clearAndUpper);
}

function clearAndUpper(text: string) {
  return text.replace(/-/, "").toUpperCase();
}

const Orphanage = () => {

  const { orfanatoId } = useParams<OrphanageParams>();
  const [orphanage, setOrphanage] = useState<OrphanageRequest>();
  const [activeImageIndex, setActiveImageIndex] = useState(0);

  useEffect(() => {
    const params: AxiosRequestConfig = {
      method: 'GET',
      url: `/orphanage/orphanages/${orfanatoId}`,
      baseURL: BASE_URL
    };
    axios(params)
      .then((response) => {
        setOrphanage(response.data);
      })
  }, []);

  if (!orphanage) {
    return <p>Carregando...</p>
  }

  return (
    <div id="page-orphanage">
      <Sidebar />

      <main>
        <div className="orphanage-details">
          <img src={orphanage.images[activeImageIndex].url} alt={orphanage.name} />

          <div className="images">
            {orphanage.images.map((image, index) => {
              return (
                <button
                  key={image.id}
                  className={activeImageIndex === index ? "active" : ""}
                  type="button"
                  onClick={() => {
                    setActiveImageIndex(index);
                  }}
                >
                  <img src={image.url} alt={orphanage.name} />
                </button>
              )
            })}
          </div>

          <div className="orphanage-details-content">
            <h1>{toPascalCase(orphanage.name)}</h1>
            <p>{orphanage.about.toUpperCase()}</p>

            <div className="map-container">
              <Map
                center={[orphanage.latitude, orphanage.longitude]}
                zoom={16}
                style={{ width: '100%', height: 280 }}
                dragging={false}
                touchZoom={false}
                zoomControl={false}
                scrollWheelZoom={false}
                doubleClickZoom={false}
              >
                <TileLayer
                  url={`https://a.tile.openstreetmap.org/{z}/{x}/{y}.png`}
                />
                <Marker interactive={false} icon={mapIcon} position={[orphanage.latitude, orphanage.longitude]} />
              </Map>

              <footer>
                <a target="_blank" rel="noopener noreferrer" href={`https://google.com/maps/dir/?api=1&destination=${orphanage.latitude},${orphanage.longitude}`}>Ver rotas no Google Maps</a>
              </footer>
            </div>

            <hr />

            <h2>Instruções para visita</h2>
            <p>{orphanage.instructions.toUpperCase()}</p>

            <div className="open-details">
              <div className="hour">
                <FiClock size={32} color="#15B6D6" />
                Segunda à Sexta <br />
                Abre as: {orphanage.abreAs}h<br />
                Fecha as: {orphanage.fechaAs}h
              </div>

              {orphanage.openOnWeekends ? (
                <div className="open-on-weekends">
                  <FiInfo size={32} color="#39CC83" />
                  Atendemos <br />
                  fim de semana
                </div>
              ) : (
                <div className="open-on-weekends dont-open">
                  <FiInfo size={32} color="#FF6690" />
                  Não Atendemos <br />
                  fim de semana
                </div>
              )}

            </div>
            <hr />
            <div style={{ margin: "0 auto", width: "40%"}}>
              <a
                type="button"
                className="contact-button"
                href={`https://api.whatsapp.com/send?l=pt_BR&phone=${orphanage.whatsapp}&text=Oi, quero conversar sobre visitas no ${orphanage.name} `}>
                {/* <FaWhatsappSquare size={50} color="green" style={{ marginBottom: '-1px' }} /> */}
                <a className="btn btn-outline-success">Entrar em contato </a>
              </a>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}

export default Orphanage;