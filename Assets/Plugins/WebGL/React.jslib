mergeInto(LibraryManager.library, {
  TrySyncWallet: function() {
    window.dispatchReactUnityEvent("TrySyncWallet");
  },
});

mergeInto(LibraryManager.library, {
  ReactGetTezos: function(amount, address) {
    window.dispatchReactUnityEvent("ReactGetTezos", amount, UTF8ToString(address));
  },
});