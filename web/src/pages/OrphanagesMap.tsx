import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { FiPlus, FiArrowRight, FiAlertOctagon } from "react-icons/fi";
import { Map, TileLayer, Marker, Popup } from "react-leaflet";

import mapIcon from "../utils/mapIcon";

import mapMakerImg from "../images/map-marker.svg";

import "../styles/pages/orphanages-map.css";
import { AllOrphanages } from "../types/AllOrphanages";
import axios, { AxiosRequestConfig } from "axios";
import { BASE_URL } from "../utils/requests";

const OrphanagesMap = () => {
  const [orphanages, setOrphanages] = useState<AllOrphanages[]>([]);

  useEffect(() => {
    const params: AxiosRequestConfig = {
      method: "GET",
      url: `/orphanage/orphanages-map`,
      baseURL: BASE_URL,
    };
    axios(params)
      .then((response) => {
        setOrphanages(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return (
    <div id="page-map">
      <aside>
        <header>
          <img src={mapMakerImg} alt="Logo da plataforma Happy" />

          <h2>Escolha um orfanato no mapa</h2>
          <p>Muitas crianças estão esperando sua visita :)</p>
        </header>

        <footer>
          <strong style={{color: "#212529"}}>RIO DE JANEIRO</strong>
          <span>Rio de Janeiro</span>
        </footer>

        <Link to="/admin/auth" style={{ textDecoration: "none"}}>
          <strong style={{color: "red"}}>ADMINISTRADOR </strong>
          <FiAlertOctagon
            size={32}
            color="red"
          />
        </Link>
      </aside>

      <Map
        center={[-22.9035, -43.2096]}
        zoom={10}
        style={{ width: "100%", height: "100%" }}
      >
        {/* Mapa alternativo: "https://api.mapbox.com/styles/v1/mapbox/light-v10/tiles/256/{z}/{x}/{y}@2x?access_token=${process.env.REACT_APP_MAPBOX_TOKEN}" */}
        <TileLayer url={`https://a.tile.openstreetmap.org/{z}/{x}/{y}.png`} />

        {orphanages.map((orphanage) => {
          return (
            <Marker
              key={orphanage.id}
              icon={mapIcon}
              position={[orphanage.latitude, orphanage.longitude]}
            >
              <Popup
                closeButton={false}
                minWidth={240}
                maxWidth={240}
                className="map-popup"
              >
                {orphanage.name}
                <Link to={`/orphanages/${orphanage.id}`}>
                  <FiArrowRight size={20} color="#FFF" />
                </Link>
              </Popup>
            </Marker>
          );
        })}
      </Map>

      <Link to="/orphanages/create" className="create-orphanage">
        <FiPlus size={32} color="#FFF" />
      </Link>
    </div>
  );
};

export default OrphanagesMap;
