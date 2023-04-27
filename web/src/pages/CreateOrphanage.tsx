import React, { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Map, Marker, TileLayer } from "react-leaflet";
import { FiPlus, FiX } from "react-icons/fi";
import { LeafletMouseEvent } from "leaflet";
import swal from 'sweetalert';
import PhoneInput from 'react-phone-number-input/input';
import api from "../services/api";

import Sidebar from "../components/SideBar";
import mapIcon from "../utils/mapIcon";

import "../styles/pages/create-orphanage.css";

interface PreviewImage {
  name: string;
  url: string;
}

const CreateOrphanage = () => {
  const history = useHistory();

  const [position, setPosition] = useState({ latitude: 0, longitude: 0 });

  const [name, setName] = useState("");
  const [about, setAbout] = useState("");
  const [instructions, setInstructions] = useState("");
  const [whatsapp, setWhatsapp] = useState("");
  const [abreAs, setAbreAs] = useState("");
  const [fechaAs, setFechaAs] = useState("");
  const [open_on_weekends, setOpenOnWeekends] = useState(true);
  const [images, setImages] = useState<File[]>([]);
  const [previewImages, setPreviewImages] = useState<PreviewImage[]>([]);

  function handleMapClick(event: LeafletMouseEvent) {
    const { lat, lng } = event.latlng;

    setPosition({
      latitude: lat,
      longitude: lng,
    });
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();

    const { latitude, longitude } = position;

    const data = new FormData();

    data.append("name", name);
    data.append("about", about);
    data.append("whatsapp", whatsapp);
    data.append("latitude", String(latitude));
    data.append("longitude", String(longitude));
    data.append("instructions", instructions);
    data.append("abreAs", abreAs);
    data.append("fechaAs", fechaAs);
    data.append("openOnWeekends", String(open_on_weekends));

    images.forEach((image) => {
      data.append("images", image);
    });

    await api
      .post("orphanage/insert", JSON.stringify(Object.fromEntries(data)))
      .then((res) => {
        console.log(res);
        swal("Orfanato cadastrado com sucesso!", "", "success");
        history.push('/app');
      })
      .catch((error) => {
        swal("Ocorreu algum erro!", "Um campo ou mais não está conforme o esperado", "error");
      });
  }

  function handleSelectImages(event: ChangeEvent<HTMLInputElement>) {
    if (!event.target.files) {
      return;
    }
    const selectedImages = Array.from(event.target.files);
    event.target.value = "";
    setImages([...images, ...selectedImages]);
    let verifyImage;

    selectedImages.forEach(e => {
      if (previewImages.length == 0) {
        verifyImage = true;
      }
      else {
        if (e.name == previewImages[previewImages.length - 1].name) {
          verifyImage = false;
        }
        else {
          verifyImage = true;
        }
      }
    });

    if (verifyImage) {
      const selectedImagesPreview = selectedImages.map((image) => {
        return { name: image.name, url: URL.createObjectURL(image) };
      });
      //quando state é um array, usar a notação de spread, criando um novo array com todos os elementos
      setPreviewImages([...previewImages, ...selectedImagesPreview]);
    }
    else {
      swal("Imagem já cadastrada", "", "error");
    }
  }

  function handleRemoveImage(image: PreviewImage) {
    const newPreviewImages = previewImages.filter(e => e.name !== image.name);
    setPreviewImages([...newPreviewImages]);
  }

  return (
    <div id="page-create-orphanage">
      <Sidebar />

      <main>
        <div className="row">
          <form onSubmit={handleSubmit} className="create-orphanage-form">
            <fieldset>
              <legend>Dados</legend>

              <Map
                center={[-23.020463, -43.475536]}
                style={{ width: "100%", height: 280 }}
                zoom={15}
                onclick={handleMapClick}
              >
                <TileLayer
                  url={`https://a.tile.openstreetmap.org/{z}/{x}/{y}.png`}
                />

                {position.latitude !== 0 && (
                  <Marker
                    interactive={false}
                    icon={mapIcon}
                    position={[position.latitude, position.longitude]}
                  />
                )}
              </Map>

              <div className="input-block col-12">
                <label htmlFor="name">Nome</label>
                <input
                  id="name"
                  value={name}
                  required={true}
                  onChange={(event) => setName(event.target.value)}
                />
              </div>

              <div className="input-block col-12">
                <label htmlFor="whatsapp">Whatsapp</label>
                <PhoneInput
                  value={whatsapp}
                  required={true}
                  onChange={(event) => setWhatsapp(String(event))}
                  defaultCountry="BR"
                  maxLength={15} />
              </div>

              <div className="input-block col-12">
                <label htmlFor="about">
                  Sobre <span>Máximo de 300 caracteres</span>
                </label>
                <textarea
                  id="name"
                  maxLength={300}
                  value={about}
                  required={true}
                  onChange={(event) => setAbout(event.target.value)}
                />
              </div>

              {/* <div className="input-block col-12">
                <label htmlFor="images">Fotos</label>

                <div className="images-container">
                  {previewImages.map((image) => {
                    return (
                      <div key={image.url}>
                        <span
                          className="remove-image"
                          onClick={() => handleRemoveImage(image)}
                        >
                          <FiX size={18} color="#ff669d" />
                        </span>
                        <img src={image.url} alt={name} className="new-image" />
                      </div>
                    );
                  })}

                  <label htmlFor="image[]" className="new-image">
                    <FiPlus size={24} color="#15b6d6" />
                  </label>
                </div>

                <input
                  type="file"
                  multiple
                  accept=".png, .jpg, .jpeg"
                  onChange={handleSelectImages}
                  id="image[]"
                />
              </div> */}
            </fieldset>

            <fieldset>
              <legend>Visitação</legend>

              <div className="input-block col-12">
                <label htmlFor="instructions">Instruções</label>
                <textarea
                  id="instructions"
                  value={instructions}
                  required={true}
                  onChange={(event) => setInstructions(event.target.value)}
                />
              </div>

              <div className="input-block col-12 input-container">
                <label htmlFor="abreAs">Horario de abertura:</label>
                <input
                  id="abreAs"
                  value={abreAs}
                  type={"text"}
                  // pattern={"\d*"}
                  step={1}
                  minLength={1}
                  maxLength={2}
                  min={6}
                  max={23}
                  required={true}
                  onChange={(event) => setAbreAs(event.target.value)}
                />
              </div>

              <div className="input-block col-12">
                <label htmlFor="fechaAs">Horario de fechamento:</label>
                <input
                  id="fechaAs"
                  value={fechaAs}
                  type={"text"}
                  // pattern={"\d*"}
                  step={1}
                  minLength={1}
                  maxLength={2}
                  min={6}
                  max={23}
                  required={true}
                  onChange={(event) => setFechaAs(event.target.value)}
                />
              </div>


              <div className="input-block col-12">
                <label htmlFor="open_on_weekends">Atende fim de semana</label>

                <div className="button-select">
                  <button
                    type="button"
                    className={open_on_weekends ? "active" : ""}
                    onClick={() => setOpenOnWeekends(true)}
                  >
                    Sim
                  </button>
                  <button
                    type="button"
                    className={!open_on_weekends ? "active" : ""}
                    onClick={() => setOpenOnWeekends(false)}
                  >
                    Não
                  </button>
                </div>

              </div>

            </fieldset>

            <button className="confirm-button" type="submit">
              Confirmar
            </button>
          </form>
        </div>
      </main>
    </div >
  );
};

export default CreateOrphanage;
