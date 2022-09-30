mergeInto(LibraryManager.library, {
  ReactGetTezos: function(amount, address) {
    window.dispatchReactUnityEvent("ReactGetTezos", amount, address);
  },
});