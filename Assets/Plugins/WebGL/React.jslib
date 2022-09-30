mergeInto(LibraryManager.library, {
  TrySyncWallet: function() {
    window.dispatchReactUnityEvent("TrySyncWallet");
  },
    ReactGetTezos: function(amount, address) {
      window.dispatchReactUnityEvent("ReactGetTezos", amount, UTF8ToString(address));
    },
});