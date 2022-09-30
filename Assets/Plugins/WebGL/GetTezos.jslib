mergeInto(LibraryManager.library, {
  GetTezos: function(amount) {
    window.dispatchReactUnityEvent("GetTezos", amount);
  },
});