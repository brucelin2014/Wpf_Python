# Dummy file to make this directory a package.

from pywinauto.application import Application

def hello():
    print("hello")

def add(a,b):
    return a+b

def open_process():
    app = Application(backend = "uia").start(r'C:\Program Files (x86)\Tencent\WeChat\WeChat.exe') # 启动微信