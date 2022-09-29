mergeInto(LibraryManager.library, {
  TrySyncWallet: function {
    window.dispatchReactUnityEvent("TrySyncWallet");
  },
});