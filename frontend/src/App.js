import React, { useState } from "react";
import axios from "axios";

export default function App() {
  // This just matches the example company in the seed data for the backend
  const [companyName] = useState("TestCompanyName");
  const [userName, setUserName] = useState(null);
  const [formUsername, setFormUsername] = useState(null);
  const [formVoucher, setFormVoucher] = useState(null);
  const [validFormVoucher, setValidFormVoucher] = useState(null);

  // Storing products here instead of getting from somewhere else
  const [products] = React.useState([
    {
      productCode: "test1",
      price: 10.95,
      title: "Cat",
      url: "https://loremflickr.com/600/600/cat",
      thumbnailUrl: "https://loremflickr.com/150/150/cat",
    },
    {
      productCode: "test2",
      price: 5.75,
      title: "Shark",
      url: "https://loremflickr.com/600/600/shark",
      thumbnailUrl: "https://loremflickr.com/150/150/shark",
    },
    {
      productCode: "test3",
      price: 8.55,
      title: "Panda",
      url: "https://loremflickr.com/600/600/panda",
      thumbnailUrl: "https://loremflickr.com/150/150/panda",
    },
    {
      productCode: "test4",
      price: 13.15,
      title: "Dog",
      url: "https://loremflickr.com/600/600/dog",
      thumbnailUrl: "https://loremflickr.com/150/150/dog",
    },
  ]);

  const [serverBasket, setServerBasket] = React.useState([]);

  // Pretend login username is passed to api and data stored against this name
  async function login(e) {
    e.preventDefault();
    setUserName(formUsername);

    const getResponse = await getServerBasket();
    setServerBasket(getResponse);
  }

  // If valid voucher discount will apply
  async function applyVoucher() {
    const getResponse = await getServerBasket(formVoucher);
    setServerBasket(getResponse);

    if (getResponse.total > getResponse.totalWithDiscount)
      setValidFormVoucher(formVoucher);
  }

  // updating the basket by setting the quantities to 0
  async function clearBasket() {
    let basketItemState = serverBasket.basketItems;
    var updateRequest = {
      userIdentifier: formUsername,
      companyIdentifier: companyName,
      basketItems: basketItemState.map((item) => {
        var req = {
          quantity: 0 - item.quantity,
          price: item.price,
          productCode: item.productCode,
        };
        return req;
      }),
    };

    const updateResponse = await axios.put(
      "https://localhost:44365/basket/update/multiple",
      updateRequest
    );

    setValidFormVoucher(null);
    setFormVoucher(null);
    const getResponse = await getRefreshedServerBasket();
    setServerBasket(getResponse);
  }

  async function addToBasket(prodCode) {
    // change to api update
    let updateRequest = null;

    if (serverBasket && serverBasket.basketItems) {
      let basketItems = serverBasket.basketItems;
      var itemToUpdate = basketItems.find((x) => x.productCode === prodCode);
      var productDetails = products.find((x) => x.productCode === prodCode);
      if (itemToUpdate) {
        updateRequest = {
          userIdentifier: formUsername,
          companyIdentifier: companyName,
          productCode: prodCode,
          price: productDetails.price,
          quantity: 1,
        };
      } else {
        updateRequest = {
          userIdentifier: formUsername,
          companyIdentifier: companyName,
          productCode: prodCode,
          price: productDetails.price,
          quantity: 1,
        };
      }
      const updateResponse = await axios.put(
        "https://localhost:44365/basket/update",
        updateRequest
      );

      const getResponse = await getServerBasket();

      setServerBasket(getResponse);
    }
  }

  async function getServerBasket(formVoucher = null) {
    const getResponse = await axios.post("https://localhost:44365/basket/get", {
      userIdentifier: formUsername,
      companyIdentifier: companyName,
      discountCode: formVoucher ? formVoucher : validFormVoucher,
    });
    return getResponse.data;
  }

  // state does not reset in time so created this separate call to ensure voucher can be reset
  async function getRefreshedServerBasket() {
    const getResponse = await axios.post("https://localhost:44365/basket/get", {
      userIdentifier: formUsername,
      companyIdentifier: companyName,
      discountCode: null,
    });
    return getResponse.data;
  }

  // Bit gross should move out into views
  function renderContent() {
    if (userName) {
      return renderStore();
    } else {
      return renderLogin();
    }
  }

  // fake login logic
  function renderLogin() {
    return (
      <div>
        <div class="w-screen h-screen flex justify-center items-center bg-gray-100">
          <form
            class="p-10 bg-white rounded flex justify-center items-center flex-col shadow-md"
            onSubmit={(e) => login(e)}
          >
            <p class="mb-5 text-3xl uppercase text-gray-600">Login</p>
            <input
              type="username"
              name="username"
              class="mb-5 p-3 w-80 focus:border-blue-700 rounded border-2 outline-none"
              autocomplete="off"
              placeholder="Username"
              required
              onChange={(e) => setFormUsername(e.target.value)}
            />
            <button
              class="bg-blue-600 hover:bg-blue-900 text-white font-bold p-2 rounded w-80"
              id="login"
              type="submit"
            >
              <span>Login</span>
            </button>
          </form>
        </div>
      </div>
    );
  }

  // used to render basket
  function renderBasket() {
    return (
      <div className="bg-blue-100 py-14">
        <h1 className="mt-8 text-center text-5xl text-blue-600 font-bold uppercase">
          Basket
        </h1>
        <div>
          <div className="container mx-auto mt-10">
            <div className="flex shadow-md my-10">
              <div className="w-3/4 bg-white px-10 py-10">
                <div className="flex mt-10 mb-5">
                  <h3 className="font-semibold text-gray-600 text-xs uppercase w-2/5">
                    Product Details
                  </h3>
                  <h3 className="font-semibold text-center text-gray-600 text-xs uppercase w-1/5">
                    Quantity
                  </h3>
                  <h3 className="font-semibold text-center text-gray-600 text-xs uppercase w-1/5">
                    Price
                  </h3>
                  <h3 className="font-semibold text-center text-gray-600 text-xs uppercase w-1/5">
                    Total
                  </h3>
                </div>
                {serverBasket.basketItems.map((item) => {
                  return (
                    <div
                      className="flex items-center hover:bg-gray-100 -mx-8 px-6 py-5"
                      key={item.productCode}
                    >
                      <div className="flex w-2/5">
                        <div className="w-32">
                          <img
                            className="h-32 rounded-xl"
                            src={
                              products.find(
                                (x) => x.productCode === item.productCode
                              ).thumbnailUrl
                            }
                            alt={item.productCode}
                          />
                        </div>
                        <div className="flex flex-col ml-4 flex-grow justify-center font-bold">
                          {
                            products.find(
                              (x) => x.productCode === item.productCode
                            ).title
                          }
                        </div>
                      </div>
                      <div className="text-center w-1/5 font-semibold text-sm">
                        {item.quantity}
                      </div>
                      <span className="text-center w-1/5 font-semibold text-sm">
                        {item.price}
                      </span>
                      <span className="text-center w-1/5 font-semibold text-sm">
                        {(item.quantity * item.price).toFixed(2)}
                      </span>
                    </div>
                  );
                })}
              </div>

              <div id="summary" className="w-1/4 px-8 py-10">
                <h1 className="font-semibold text-2xl border-b pb-2">
                  Summary
                </h1>
                <div className="py-10">
                  <label
                    for="promo"
                    className="font-semibold inline-block mb-3 text-sm uppercase"
                  >
                    Discount Code
                  </label>
                  <input
                    type="text"
                    id="promo"
                    placeholder="Enter your code"
                    className="p-2 text-sm w-full"
                    onChange={(e) => setFormVoucher(e.target.value)}
                  />
                </div>
                <button
                  className="bg-blue-500 rounded-full hover:bg-blue-600 px-5 py-2 text-sm text-white uppercase"
                  onClick={() => applyVoucher()}
                >
                  Apply
                </button>
                <div className="border-t mt-8">
                  <div className="flex font-semibold justify-between py-6 text-sm uppercase">
                    <span>Sub Total</span>
                    <span>£{serverBasket.total.toFixed(2)}</span>
                  </div>
                  <div className="flex font-semibold justify-between py-6 text-sm uppercase">
                    <span>Total Discount</span>
                    <span>
                      £
                      {(
                        serverBasket.total - serverBasket.totalWithDiscount
                      ).toFixed(2)}
                    </span>
                  </div>
                  <div className="flex font-semibold justify-between py-6 text-sm uppercase">
                    <span>Total Cost</span>
                    <span>£{serverBasket.totalWithDiscount.toFixed(2)}</span>
                  </div>
                  <button
                    className="bg-blue-500 rounded-full font-semibold hover:bg-blue-600 py-3 text-sm text-white uppercase w-full"
                    onClick={() => clearBasket()}
                  >
                    Clear Basket
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  // used to render the product voew
  function renderStore() {
    return (
      <div>
        {serverBasket &&
        serverBasket.basketItems &&
        serverBasket.basketItems.length > 0
          ? renderBasket()
          : ""}

        <div className="bg-blue-100 py-14">
          <h1 className="mt-8 text-center text-5xl text-blue-600 font-bold uppercase">
            Sponsor a pet
          </h1>
          <div className="md:flex md:justify-center md:space-x-8 md:px-14">
            {products.map((prod) => {
              return (
                <div
                  key={prod.productCode}
                  className="mt-16 py-4 px-4 bg-whit w-72 bg-white rounded-xl shadow-lg hover:shadow-xl transform hover:scale-110 transition duration-500 mx-auto md:mx-0"
                >
                  <div className="w-sm">
                    <img
                      className="w-64"
                      src={prod.url}
                      alt={prod.productCode}
                    />
                    <div className="mt-4 text-blue-600 text-center">
                      <h1 className="text-xl font-bold">{prod.title}</h1>
                      <p className="mt-4 text-gray-600">{prod.price}</p>
                      <button
                        className="mt-8 mb-4 py-2 px-14 rounded-full uppercase bg-blue-600 text-white tracking-widest hover:bg-blue-500 transition duration-200"
                        onClick={() => addToBasket(prod.productCode)}
                      >
                        Add to basket
                      </button>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>
    );
  }

  return (
    <div>
      <main>{renderContent()}</main>
    </div>
  );
}
