mergeInto(LibraryManager.library, {
    ReactGetTezos: function(amount, address) {
      window.dispatchReactUnityEvent("ReactGetTezos", amount, UTF8ToString(address));
    },
	BuyHat: function(){
		window.dispatchReactUnityEvent("BuyHat");
	}
});