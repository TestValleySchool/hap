﻿(function(n){typeof define=="function"&&define.amd?define(["jquery"],n):n(jQuery)})(function(n){function r(n){var u,f,r;u=n.target.offsetWidth,f=n.target.offsetHeight,r={distX:n.distX,distY:n.distY,velocityX:n.velocityX,velocityY:n.velocityY,finger:n.finger},n.distX>n.distY?n.distX>-n.distY?(n.distX/u>t.threshold||n.velocityX*n.distX/u*t.sensitivity>1)&&(r.type="swiperight",i(n.currentTarget,r)):(-n.distY/f>t.threshold||n.velocityY*n.distY/u*t.sensitivity>1)&&(r.type="swipeup",i(n.currentTarget,r)):n.distX>-n.distY?(n.distY/f>t.threshold||n.velocityY*n.distY/u*t.sensitivity>1)&&(r.type="swipedown",i(n.currentTarget,r)):(-n.distX/u>t.threshold||n.velocityX*n.distX/u*t.sensitivity>1)&&(r.type="swipeleft",i(n.currentTarget,r))}function u(t){var i=n.data(t,"event_swipe");return i||(i={count:0},n.data(t,"event_swipe",i)),i}var f=n.event.add,e=n.event.remove,i=function(t,i,r){n.event.trigger(i,r,t)},t={threshold:.4,sensitivity:6};n.event.special.swipe=n.event.special.swipeleft=n.event.special.swiperight=n.event.special.swipeup=n.event.special.swipedown={setup:function(n){var n=u(this);if(!(n.count++>0))return f(this,"moveend",r),!0},teardown:function(){var n=u(this);if(!(--n.count>0))return e(this,"moveend",r),!0},settings:t}})