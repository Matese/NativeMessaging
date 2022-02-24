/*
On a click on the browser action, send the app a message.
*/
browser.browserAction.onClicked.addListener(async () => {
  console.log("Sending: test");
  
  const result = await browser.runtime
    .sendNativeMessage("ping_ponger", "ping")
    //.sendNativeMessage("ping_ponger", { text: 'ping' })
    .catch((error) => {
      console.error(error);
    });

  console.log(result);
});